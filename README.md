Projekt Leírása

A projekt egy egyetemi rendszerfejlesztési kurzus keretében készült. Célja a Moodle weboldal megvalósítása.


Használt Technológiák:

C# 
Entity Framework
ASP.NET
SQLite (moodle.db)
TypeScript
React
HTML
JavaScript
CSS



Használati Útmutató - Frissen Telepített Windows Operációs Rendszerhez

Előfeltételek:

Telepítse a .NET SDK futtató környezetét. Az alkalmazás target frameworkje .NET 8.0. https://dotnet.microsoft.com/en-us/download/visual-studio-sdks

Telepítse a Node.js legfrissebb LTS verzióját. https://nodejs.org/en

Telepítse a Git legfrissebb verzióját. https://git-scm.com/download/win

Telepítse a Visual Studio legfrissebb verzióját. https://visualstudio.microsoft.com/downloads/



Telepítési és Indítási Útmutató
1. Projekt Klónozása

Nyissa meg a parancssort és klónozza le a projektet a GitHubról a következő paranccsal:

bash

git clone https://github.com/skiki0819/VEMISAB223RF_Team2.git

3. Telepítse a szükséges NuGet csomagokat Visual Studioban:

Microsoft.AspNetCore.Cors

Microsoft.EntityFrameworkCore

Microsoft.EntityFrameworkCore.Design

Microsoft.EntityFrameworkCore.Sqlite

Swashbuckle.AspNetCore


5. Szerver Telepítése és Indítása

Navigáljon a Szerver mappába a projekt gyökérkönyvtárában:

bash

cd repository/Moodle.Server/Moodle.Server/Moodle.Server

dotnet restore

dotnet run

Mostantól a Moodle API swagger elindul a böngészőben a http://localhost:5191 címen.



8. Kliens Telepítése és Indítása

Navigáljon a Kliens mappába a projekt gyökérkönyvtárában:

bash

cd repository/Moodle.Client

bash

npm install

npm run dev

A terminalban megjelent localhost címen elérhető a kliens weboldal. pl: http://localhost:5173




Szerkesztők:

Albert Szebasztián

Góczán Dávid

Sándor Krisztián






