dotnet publish -c release -r linux-x64 --self-contained -p:PublishSingleFile=true
scp -i ${ENV:TL_IDENTITY_PATH} .\bin\Release\net8.0\linux-x64\publish\* ${ENV:TL_USER}${ENV:TL_ADDRESS}:${ENV:TL_UPLOAD_PATH}
ssh ${ENV:TL_USER}${ENV:TL_ADDRESS} './update-bot.sh; exit'
