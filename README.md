# Amega.Integration.Api

Solution contains 2 Projects

## Amega.Integration.Api

 Amega.Integration.Api is server  which privides endpoint  to get a list of available financial instruments and CRUD functionality
  Excample to Get available financial instrument
  ```
  curl -X 'GET' \
  'https://localhost:7179/FinancialInstruments/GetBySymbol?symbol=BTCUSD'
```
  
 Excample to Get financial instrument prices sourced from a public data provider 
 ```
  curl -X 'GET' \
    'https://localhost:7179/api/InstrumentPrice/GetInstrumentPrice?symbol=EURUSD'
```

Before make requests  change api key on app config
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Tiingo": {
    "APIToken": "API_TOKEN",
    "ApiURL": "https://api.tiingo.com/tiingo/fx"
  },
  "AllowedHosts": "*"
}
```
 
## Amega.Integration.Client
 Amega.Integration.Client is  excemple how to subscripe financial instrument price real time update
 
