Atelier Test - Backend (ASP.NET Core)

Description : API REST développée en ASP.NET Core permettant de gérer des joueurs de tennis et de calculer des statistiques.

Technologies utilisées :.NET 8, ASP.NET Core, Web API, System.Text.Json, Swagger

Architecture :
Controllers/
Services/
Models/
DTOs/
Data/

Fonctionnalités
Players
GET /players : liste des joueurs triés par ranking
GET /players/{id} : détail d’un joueur
POST /players : ajouter un joueur
Statistics
GET /stats :
pays avec le meilleur ratio de victoires
IMC moyen des joueurs
médiane des tailles

Lancer le projet :
dotnet restore
dotnet build
dotnet run

Données
Le fichier headtohead.json doit être placé ici : Data/headtohead.json



