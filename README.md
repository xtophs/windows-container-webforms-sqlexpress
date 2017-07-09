# ASP.NET WebForms app running in a Windows Server Container

## Overview
The app is V1.0 an old WebForms app from an [ASP.NET starter kit](https://www.asp.net/downloads/starter-kits/the-beer-house) called The Beer House. The name was appealing.

The app is running on .NET Framework 2.0

The [original code is available on CodePlex](http://thebeerhouse.codeplex.com/releases/view/127) (until Dec 2017)  

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

