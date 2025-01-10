# FXExchange

## Description
This project is designed to handle currency conversions using predefined exchange rates or live rates via an API.



## Requirements
To run this project, with mock date you need:
  - Configure appsettings.json 
     - copy the appsettings.json.template file to create appsettings.json in the same directory.
     - property <UseMockData> set true
       
To run this project, with live rate you need: 
  - An API key from https://www.exchangerate-api.com/
  - Configure appsettings.json
     - do copy the appsettings.json.template file to create appsettings.json in the same directory.
     - replace <ApiKey> in appsettings.json file with your actual API key.
     - property <UseMockData> set false
