# ASP.NET WebForms app running in a Windows Server Container

## Overview
The app is V1.0 an old WebForms app from an [ASP.NET starter kit](https://www.asp.net/downloads/starter-kits/the-beer-house) called The Beer House. The name was appealing.

The app is running on .NET Framework 2.0. **No code changes** were necessary to run the app inside a windows container. The only change between the [original code](http://thebeerhouse.codeplex.com/releases/view/127) and this repo is the connection string to point to the local SQL Express database inside the container. This could just as easily point to Azure SQL DB, but I wanted to demonstrating an app as-is.

## Helpful Info
Lauch the container
```
docker run -p 8000:8000 windows-container-webforms-sqlexpress:latest
```

Credentials for the The SQLExpress database inside the container:
```
sa
P@ssword
```

Base container by [@samslaider](https://hub.docker.com/u/samslaider/)

