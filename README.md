# Atelier Test - Backend Tennis API

## Présentation

API REST développée en **.NET 8** permettant de gérer des joueurs de tennis et de calculer différentes statistiques à partir du fichier fourni.

## Technologies utilisées

* .NET 8
* ASP.NET Core Web API
* JSON
* Swagger

## Fonctionnalités

### Récupérer la liste des joueurs

```http
GET /players
```

Retourne la liste des joueurs triée du meilleur au moins bon classement.

### Récupérer un joueur par son identifiant

```http
GET /players/{id}
```

Retourne les informations détaillées d'un joueur.

### Récupérer les statistiques

```http
GET /stats
```

Retourne :

* Le pays ayant le meilleur ratio de victoires
* L'IMC moyen des joueurs
* La médiane des tailles des joueurs

### Ajouter un joueur

```http
POST /players
```

Ajoute un nouveau joueur à la collection.

## Installation locale

```bash
git clone https://github.com/ManelMnif96/TennisTest.git
cd TennisTest
dotnet restore
dotnet run
```

## Déploiement

Application disponible à l'adresse :

https://tennistest-1.onrender.com

## Documentation Swagger

https://tennistest-1.onrender.com/swagger
