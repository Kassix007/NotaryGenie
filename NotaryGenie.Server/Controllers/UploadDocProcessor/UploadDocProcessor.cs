
using Microsoft.AspNetCore.Mvc;
using NotaryGenie.Server.Services.Documents;
using NotaryGenie.Server.Services.OCRProcessors;
using System.Collections.Generic;

namespace NotaryGenie.Server.Controllers.UploadDocProcessor
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IBirthCertService _birthCertService;
        private readonly ICWAService _cWAService;
        private readonly IIdCardFrontService _idCardFrontService;
        private readonly IIdCardBackService _idCardBackService;
        

        public FileController(IFileService fileService, IBirthCertService birthCertService, ICWAService cWAService, IIdCardBackService idCardBackService, IIdCardFrontService idCardFrontService)
        {
            _fileService = fileService;
            _birthCertService = birthCertService;
            _cWAService = cWAService;
            _idCardFrontService = idCardFrontService;
            _idCardBackService = idCardBackService;
        }

        [HttpGet("InfoFromFiles")]
        public async Task<ActionResult<IEnumerable<string>>> GetFilesInfoAsync()
        {
            var files = _fileService.GetFilesFromTempStorage();
            if (files.Count == 0)
            {
                return NotFound("No files found in TempDocumentStorage.");
            }

            //paths variables
            string nationalIdFrontPath = null;
            string nationalIdBackPath = null;
            string birthCertificatePath = null;
            string marriageCertificatePath = null;
            string proofOfAddressCEBPath = null;
            string proofOfAddressCWAPath = null;

            // Initialize NID values
            string firstNameNID = string.Empty;
            string surnameNID = string.Empty;
            string dateOfBirthNID = string.Empty;
            string genderNID = string.Empty;
            string idNumber = string.Empty;
            string idNumBack = string.Empty;
            string NIDIssueDate = string.Empty;

            // Init Birth Cert Values
            string birthCertNum = string.Empty;
            string childSurname = string.Empty;
            string childOtherNames = string.Empty;
            string childIDNum = string.Empty;
            string dateOfBirthBirthCert = string.Empty;

            // Proof Of Address
            string poaName = string.Empty;
            string address = string.Empty;


            foreach (var file in files)
            {
                if (file.FileName.Contains("nationalIdFront", StringComparison.OrdinalIgnoreCase))
                {
                    nationalIdFrontPath = file.FilePath;
                }
                else if (file.FileName.Contains("birthCertificate", StringComparison.OrdinalIgnoreCase))
                {
                    birthCertificatePath = file.FilePath;
                }
                else if (file.FileName.Contains("marriageCertificate", StringComparison.OrdinalIgnoreCase))
                {
                    marriageCertificatePath = file.FilePath;
                }
                else if (file.FileName.Contains("proofOfAddress", StringComparison.OrdinalIgnoreCase))
                {
                    if (file.FileName.Contains("CEB", StringComparison.OrdinalIgnoreCase))
                    {
                        proofOfAddressCEBPath = file.FilePath;
                        //call CEB service
                    }
                    else if (file.FileName.Contains("CWA", StringComparison.OrdinalIgnoreCase))
                    {
                        proofOfAddressCWAPath = file.FilePath;
                        var cwaEntities = await _cWAService.ProcessDocumentAsync(proofOfAddressCWAPath);

                        // Process CWA entities here
                        foreach (var entity in cwaEntities)
                        {
                            switch (entity.Type)
                            {
                                case "name":
                                    poaName = entity.MentionText;
                                    break;
                                case "address":
                                    address = entity.MentionText;
                                    break;
                            }
                        }
                    }
                }
                else if (file.FileName.Contains("nationalIdBack", StringComparison.OrdinalIgnoreCase))
                {
                    nationalIdBackPath = file.FilePath;
                }
            }

            // Start all tasks in parallel
            var idEntitiesTask = _idCardFrontService.ProcessDocumentAsync(nationalIdFrontPath);
            var idBackEntitiesTask = _idCardBackService.ProcessDocumentAsync(nationalIdBackPath);
            var birthCertEntitiesTask = _birthCertService.ProcessDocumentAsync(birthCertificatePath);

            // Wait for all tasks to complete
            await Task.WhenAll(idEntitiesTask, idBackEntitiesTask, birthCertEntitiesTask);

            // Retrieve the results after all tasks have completed
            var idEntities = await idEntitiesTask;
            var idBackEntities = await idBackEntitiesTask;
            var birthCertEntities = await birthCertEntitiesTask;


            // Process ID card front entities
            foreach (var entity in idEntities)
            {
                switch (entity.Type)
                {
                    case "firstName":
                        firstNameNID = entity.MentionText;
                        break;
                    case "surname":
                        surnameNID = entity.MentionText;
                        break;
                    case "dateOfBirth":
                        dateOfBirthNID = entity.MentionText;
                        break;
                    case "idNumber":
                        idNumber = entity.MentionText;
                        break;
                    case "gender":
                        genderNID = entity.MentionText;
                        break;
                }
            }

            // Process ID card back entities
            foreach (var entity in idBackEntities)
            {
                switch (entity.Type)
                {
                    case "NIDIssueDate":
                        NIDIssueDate = entity.MentionText;
                        break;
                    case "NIDNumber":
                        idNumBack = entity.MentionText;
                        break;
                }
            }

            // Process birth certificate entities
            foreach (var entity in birthCertEntities)
            {
                switch (entity.Type)
                {
                    case "certificateNumber":
                        birthCertNum = entity.MentionText;
                        break;
                    case "childIDNumber":
                        childIDNum = entity.MentionText;
                        break;
                    case "childOtherNames":
                        childOtherNames = entity.MentionText;
                        break;
                    case "childSurname":
                        childSurname = entity.MentionText;
                        break;
                    case "dateOfBirth":
                        dateOfBirthBirthCert = entity.MentionText;
                        break;
                }
            }

            String firstNameMsg = String.Empty;
            if (childOtherNames.Contains(firstNameNID)) 
            {
                firstNameMsg = "Other names matches National ID Card and Birth Certificate";
            }
            else 
            {
                firstNameMsg = "Name does not match on National ID Card and Birth Certificate";
            }
            String surnameMsg = String.Empty;   
            if (childSurname.Equals(surnameNID))
            {
                surnameMsg = "Surname matches National ID and Birth Certificate";
            }
            else
            {
                surnameMsg = "Surname does not match National ID Card and Birth Certificate";
            }


            // Return the extracted values as a JSON response
            return Ok(new
            {
                firstNameNID,
                surnameNID,
                dateOfBirthNID,
                idNumber,
                genderNID,
                NIDIssueDate,
                idNumBack,

                birthCertNum,
                childSurname,
                childOtherNames,
                childIDNum,
                dateOfBirthBirthCert,

                poaName,
                address
            });
        }


    }
}