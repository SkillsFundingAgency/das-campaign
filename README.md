# das-campaign

[![Build Status](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_apis/build/status/das-campaign)](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/latest?definitionId=1232)

[www.apprenticeships.gov.uk](https://www.apprenticeships.gov.uk)

# Requirements

DotNet Core 2.2 and any supported IDE for DEV running.

Azure Storage Emulator

## About

The das-campaign solution uses data from contentful, Vacancies API and DAS APIM to provide a hub for information on Apprenticeships, for employers and apprentices

## Local running

You must have the Azure Storage emulator running, and in that a table created called `Configuration` in that table add the following:

PartitionKey: LOCAL

RowKey: SFA.DAS.Campaign_1.0

Data:
```
{
    "EmployerAccountBaseUrl": "https://accounts.eas.apprenticeships.education.gov.uk/",
    "ContentfulOptions": {
        "MaxNumberOfRateLimitRetries": 0,
        "PreviewApiKey": "12345",
        "UsePreviewApi": true,
        "SpaceId": "12345",
        "DeliveryApiKey": "12345"
    },
    "Mapping": {
        "StaticHeight": "120",
        "StaticWidth": "190",
        "ClientID": " ",
        "PrivateKey": " 12345",
        "ApiKey": "12345"
    },
    "FatBaseUrl": "https://findapprenticeshiptraining.apprenticeships.education.gov.uk/",
    "ConnectionStrings": {
        "ContentCacheDatabase": "",
        "SharedRedis": " "
    },
    "QueueConnectionString": "UseDevelopmentStorage=true",
    "VacanciesApi": {
        "ApiKey": "1234567890",
        "BaseUrl": "https://{vacancies-apim}/vacancies-at"
    },
    "Postcode": {
        "Url": "http://api.postcodes.io/"
    },
    "OuterApi": {
        "Key": "1234567890",
        "BaseUrl": "https://{APIM}/campaign/"
    },
    "UserDataQueueNames": {
        "RemoveUserDataQueueName": "sfa-das-cpg-unsubscribe",
        "StoreUserDataQueueName": "sfa-das-cpg-subscribe"
    },
    "UserDataCryptography": {
        "UserUrlSalt": "1234",
        "AllowedUrlCharacters": "ABCDEF",
        "UserUrlMinValue": 10
    }
}

```

You are able to get APIM keys for the Outer API and Vacancies if you work within the agency. A key for contenful api and google maps is also required for full running of the site.