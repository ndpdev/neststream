# NestStream

NestStream is a .NET Core 2.1 client consuming Server-Sent Events for your Nest devices through the [REST Streaming API](https://developers.nest.com/guides/api/rest-streaming-guide).

## Using NestStream

Update the **appsettings.json** file and modify the **NestApi** values from the table below. NestStream will not run without providing a valid AccessToken.

| Key          | Description |
| ------------ | ------- |
| StreamingUri | Nest API endpoint and [Path Filter](#path-filters) |
| AccessToken  | [Private Token](#generating-a-private-token) generated for your client|


## Example

When the application starts or an SSE response is received, a summary of the response is printed:

```
$ dotnet run
7:52 PM [StreamEvent: /devices]
   [Thermostat Hallway (upstairs)] ID:peyiJNo0IldT2YlIVtYaGQ Online:True Temperature:69 Humidity:45 HvacMode:eco HvacState:heating FanActive:False
8:02 PM [StreamEvent: /devices]
   [Thermostat Hallway (upstairs)] ID:peyiJNo0IldT2YlIVtYaGQ Online:True Temperature:68 Humidity:45 HvacMode:eco HvacState:heating FanActive:False
```

## Path Filters

By default, NestStream uses `https://developer-api.nest.com/devices` for its *StreamingUri* value. The `/devices` path only returns nodes under **devices** in the [Nest Data Model](include/nest_data_model.json). NestStream works with the following filters:

| Path                       |
| -------------------------- |
| `/`                        |
| `/metadata`                |
| `/devices`                 |
| `/devices/thermostats`     |
| `/devices/smoke_co_alarms` |
| `/devices/cameras`         |
| `/structures`              |

## Generating a Private Token

Follow the guide at [OAuth 2.0 Authentication and Authorization](https://developers.nest.com/guides/api/how-to-auth) to generate a private access token for your client. The following PowerShell script can be used once you've registered a client to generate an access token.

```powershell
# The following values must be changed to your OAuth "Client ID" and "Client Secret"
# Ref: https://developers.nest.com/guides/account-management/register-client

$clientID = 'your-client-id'
$clientSecret = 'your-client-secret'

$authReqUri = 'https://home.nest.com/login/oauth2?client_id={0}&state=STATE' -f $clientID, $stateToken

$pincode = Read-Host "Enter the code generated at $authReqUri"

$tokenReqHeaders = @{'Content-Type'='application/x-www-form-urlencoded'}
$tokenReqBody = @{'client_id'=$clientID; 'client_secret'=$clientSecret; 'code'=$pincode; 'grant_type'='authorization_code'}
$tokenResp = Invoke-WebRequest -Uri 'https://api.home.nest.com/oauth2/access_token' -UseBasicParsing -Method Post -Headers $tokenReqHeaders -Body $tokenReqBody
$oauthToken = ($tokenResp.Content | ConvertFrom-Json).access_token

Write-Host "Your access token is: $oauthToken"
```

If successful, the access token will be a long string that starts similar to: `c.cya9lrmcOP...`