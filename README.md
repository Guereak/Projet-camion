# TransConnect Management System

## Description
Ce projet est une application C# destinée à la gestion des salariés, des clients, et des commandes pour la société de transports routiers TransConnect. Le projet inclut plusieurs modules pour gérer les clients, les salariés, les commandes, et fournir des statistiques détaillées.

## Fonctionnalités
- Gestion des clients : ajouter, modifier, supprimer, afficher
- Gestion des salariés : embaucher, licencier, afficher l'organigramme
- Gestion des commandes : créer, modifier, simuler les étapes d'une commande
- Statistiques : bilans généraux, historique des livraisons, analyses des commandes
- Utilisation de concepts avancés de C# : POO, héritage, classe abstraite, interface, délégation, collections génériques, arbre n-aire, algorithme de Dijkstra

## Installation
1. Clonez le repository :
    ```sh
    git clone https://github.com/Guereak/Projet-camion
    ```
2. Ouvrez le projet dans votre IDE C# préféré (Visual Studio recommandé).
3. Restaurez les packages NuGet si nécessaire.
4. Compilez le projet pour vérifier qu'il n'y a pas d'erreurs.

## Utilisation
- Lancez l'application depuis votre IDE.
- Utilisez le menu principal pour naviguer entre les différents modules.
- Suivez les instructions à l'écran pour gérer les clients, les salariés, et les commandes.

## Modules

### Module Client
- Permet d’entrer, supprimer ou modifier un nouveau Client depuis la console ou depuis un fichier.
- Affichage de l’ensemble des Clients par ordre alphabétique, par ville, ou par montant des achats cumulé.

### Module Salarié
- Affiche l’organigramme de manière efficace basé sur une construction d’arbre n-aire.
- Permet d'embaucher ou licencier un salarié et de l’inclure ou l’exclure de l’organigramme.

### Module Commande
- Permet de créer, modifier et simuler les étapes d’une commande.
- Assure la disponibilité des chauffeurs et calcule le chemin le plus court pour la livraison.
- Calcule le prix d’une commande en fonction du kilométrage et du tarif horaire du chauffeur.

### Module Statistiques
- Affiche par chauffeur le nombre de livraisons effectuées.
- Affiche les commandes selon une période de temps.
- Affiche la moyenne des prix des commandes et des comptes clients.
- Affiche la liste des commandes pour un client.
