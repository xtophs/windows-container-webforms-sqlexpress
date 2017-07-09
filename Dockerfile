FROM samslaider/mssql-iis-windows:v0.5
LABEL Name=windows-container-webforms-sqlexpress Version=0.0.1 

# Add the content
RUN mkdir c:\web
ADD ./TheBeerHouse /web

# Configure SQL Express for the ASP.NET Database
RUN sqlcmd -q "exec sp_attach_db @dbname=N'TheBeerHouse', @filename1=N'C:\web\App_data\aspnetdb.mdf', @filename2=N'C:\web\App_Data\aspnetdb_Log.ldf'"

# configure the web site
EXPOSE 8000
RUN powershell -NoProfile -Command \
    Import-module IISAdministration; \
    New-IISSite -Name "Site" -PhysicalPath C:\web -BindingInformation "*:8000:"


    




