﻿using SchoolBus.WS.CCW.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace SchoolBus.WS.CCW.Facade.Service
{
    public class CCWService : ICCWService
    {
        public readonly CVSECommonClient Client;
        private readonly string userId;
        private readonly string guid;
        private readonly string directory;
        private readonly string endpointURL;
        private readonly string applicationIdentifier;
        private readonly string basicAuth_username;
        private readonly string basicAuth_password;
        
        public CCWService (string userId, string guid, string directory, string endpointURL, string applicationIdentifier, string basicAuth_username, string basicAuth_password)
        {
            this.userId = userId;
            this.guid = guid;
            this.directory = directory;
            this.endpointURL = endpointURL;
            this.applicationIdentifier = applicationIdentifier;
            this.basicAuth_username = basicAuth_username;
            this.basicAuth_password = basicAuth_password;            

            this.Client = new CVSECommonClient(GetBinding(), new EndpointAddress(this.endpointURL));

            this.Client.ClientCredentials.UserName.UserName = this.basicAuth_username;
            this.Client.ClientCredentials.UserName.Password = this.basicAuth_password;
        }

        public VehicleDescription GetBCVehicleForSerialNumber(string serialNumber)
        {
            var task = this.Client.getBCVehicleForSerialNumberAsync(serialNumber, userId, guid, directory, applicationIdentifier);
            task.Wait();
            return task.Result;            
        }

        public VehicleDescription GetBCVehicleForRegistrationNumber(string registrationNumber)
        {
            var task = this.Client.getBCVehicleForSerialNumberAsync(registrationNumber, userId, guid, directory, applicationIdentifier);
            task.Wait();
            return task.Result;            
        }


        public VehicleDescription GetBCVehicleForLicensePlateNumber(string licensePlateNumber)
        {
            var task = this.Client.getBCVehicleForLicensePlateNumberAsync(licensePlateNumber, userId, guid, directory, applicationIdentifier);
            task.Wait();
            return task.Result;            
        }

        public VehicleDescription GetBCVehicleForDecalNumber(string decalNumber)
        {
            var task = this.Client.getBCVehicleForDecalNumberAsync(decalNumber, userId, guid, directory, applicationIdentifier);
            task.Wait();
            return task.Result;
        }

        private static BasicHttpBinding GetBinding()
        {
            HttpTransportSecurity transport = new HttpTransportSecurity();
            transport.ClientCredentialType = HttpClientCredentialType.Basic;           

            return new BasicHttpBinding()
            {
                Security = { Mode = BasicHttpSecurityMode.Transport, Transport = transport }                
            };
        }
    }
}