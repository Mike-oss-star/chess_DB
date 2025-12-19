# RAPPORT DU PROJET 

## CHESS_DB 

![ECAM](Logo.png)

### Paticipants au projet 

KENFACK FOKOUA Steve Aryann  23230 ;
LOYIM TCHIOFOUO Mike Branly 23390 ;





# Introduction :

La gestion des compétitions sportives au sein d’une fédération nécessite des outils informatiques capables de centraliser les informations, de faciliter le travail administratif et de garantir la fiabilité des données. 

Dans ce contexte, l’informatisation des processus de gestion apparaît comme une solution essentielle pour améliorer l’efficacité et la cohérence des activités organisationnelles.

Ce projet s’inscrit dans une démarche de conception logicielle visant à appliquer les notions théoriques vues en cours à un cas concret, tout en respectant les principes fondamentaux de qualité logicielle tels que la modularité, la maintenabilité et la réutilisabilité.

# Description du projet :

Le projet consiste à concevoir et développer une application de gestion des matchs pour une fédération d’échecs, destinée à être utilisée par le personnel administratif. 

L’application permettra de gérer les informations personnelles des joueurs, l’organisation des compétitions, l’inscription des participants, ainsi que l’encodage des parties jouées avec leurs coups et leurs résultats.

Le système intégrera également le calcul automatique du classement ELO des joueurs et assurera la persistance de l’ensemble des données. 

Enfin, l’architecture de l’application sera conçue de manière à pouvoir être adaptée à d’autres jeux ou disciplines sportives, en séparant les fonctionnalités génériques des règles spécifiques aux échecs, afin de favoriser la réutilisation du code développé.

# Principe SOLID :
Dans le cadre de notre projet, nous avons veillé à appliquer les principes SOLID, et plus particulièrement le **Single Responsibility Principle (SRP)** et le **Liskov Substitution Principle (LSP)**, afin de produire un code clair, modulaire et maintenable.

## Single Responsibility Principle (SRP)

Nous avons structuré notre `CompetitionService` pour qu’il se concentre uniquement sur la gestion des compétitions, incluant la lecture, l’ajout, la modification et la suppression des données. Cette organisation nous permet de séparer nettement la logique métier de la présentation et de la navigation, qui sont prises en charge par le `AddCompetitionPageViewModel` et un service de navigation dédié.  
En respectant ce principe, chaque classe dispose d’une responsabilité unique, ce qui améliore la lisibilité, la testabilité et facilite l’évolution du code sans risques de couplage excessif.

## Liskov Substitution Principle (LSP)

Même si nous n’utilisons pas d’héritage direct dans ce projet, nous avons conçu le service de gestion des compétitions de manière à pouvoir le substituer facilement par une autre implémentation, telle qu’un stockage en base de données. Cette approche garantit que tout remplacement futur du service ne modifiera pas le comportement attendu par le ViewModel, assurant ainsi la robustesse, la flexibilité et la réutilisabilité de notre application.

## Conclusion

Ainsi, notre architecture démontre une forte adhésion aux principes **SRP** et **LSP** : chaque classe a une responsabilité clairement définie et le service de gestion des compétitions peut évoluer indépendamment, ce qui nous permet de produire un code stable, modulable et facilement extensible.

# Suivi de l'évolution du classement Elo d'un joueur

Nous avons développé une fonctionnalité permettant de **suivre l’évolution du classement Elo d’un joueur** de manière détaillée et chronologique.

## Fonctionnement

- **Sélection du joueur** :  
  Lorsqu’un joueur est sélectionné, le système affiche automatiquement toutes les parties qu’il a disputées.

- **Informations affichées pour chaque partie** :  
  - **Date de la partie**  
  - **Nom de l’adversaire**  
  - **Résultat de la partie** (victoire, défaite, match nul)  
  - **Classement Elo avant la partie**  
  - **Classement Elo après la partie**

- **Calcul du classement Elo** :  
  Le système utilise la formule standard Elo pour mettre à jour le classement après chaque partie, en tenant compte du score attendu et du score réel.

## Bénéfices

- **Analyse des performances individuelles** : Permet d’identifier les tendances et l’évolution du joueur sur le temps.  
- **Transparence du calcul Elo** : Chaque modification du classement est visible et traçable.  
- **Support à la prise de décision** : Utile pour les entraîneurs et organisateurs afin d’évaluer les joueurs et planifier des stratégies.  
- **Interface intuitive** : L’utilisateur accède rapidement à un historique clair et détaillé, facilitant la consultation et l’interprétation des données.

---

**Résumé** :  
Cette fonctionnalité offre une **vision complète et détaillée du parcours d’un joueur**, en combinant résultats, adversaires et évolution du classement Elo, permettant une **analyse précise et immédiate des performances**.


# Adaptabilité de l'application à d'autres fédérations sportives

Nous considérons que l’application que nous avons développée présente un **haut degré d’adaptabilité à d’autres fédérations sportives**. Cette adaptabilité repose sur plusieurs éléments fondamentaux de l’architecture et de la conception logicielle.

## 1. Architecture MVVM

Nous avons structuré notre application selon le **modèle MVVM (Model-View-ViewModel)**, ce qui permet de :

- Séparer clairement la **logique de présentation**, la **logique métier** et l’**accès aux données**.
- Permettre aux **ViewModels** d’interagir exclusivement avec des **services dédiés** (`PlayerService`, `GameService`, `CompetitionService`) sans manipuler directement les données persistées.

> Cette séparation garantit que l’adaptation à une nouvelle fédération nécessite uniquement des modifications au niveau des services, sans impacter la logique d’interface utilisateur.

## 2. Modèles de données génériques

Les **entités principales** — joueurs, parties et compétitions — ont été définies de manière générique, incluant des propriétés standard telles que :

- `Id`
- `Name`
- `Elo`
- `Result`
- `Capacity`

> Cette standardisation permet de réutiliser les mêmes structures de données pour différentes fédérations, à condition que les concepts de base restent similaires.

## 3. Fonctionnalités dynamiques de filtrage et de recherche

L’application inclut des **mécanismes dynamiques** pour le filtrage et la recherche, basés sur des **critères paramétrables** et non codés en dur.

> Ces fonctionnalités permettent de gérer des variations dans les types de compétitions, les catégories de joueurs ou d’autres critères propres à une nouvelle fédération sans modifications structurelles importantes.

## 4. Navigation centralisée

La navigation est gérée par le `MainViewModel`, ce qui garantit que :

- L’adaptation à une autre fédération ne nécessite aucun ajustement de la **structure de navigation**.
- Les liaisons entre **vues et ViewModels** restent intactes.

## 5. Flexibilité et maintenabilité

Si la fédération cible introduit des règles métier spécifiques (par ex. :

- Systèmes de classement alternatifs,
- Catégories de joueurs spécifiques,
- Structures de compétition particulières),

> seules les parties relatives aux **services et aux modèles de données** devront être adaptées, sans impacter l’interface utilisateur.

## Conclusion

L’architecture modulaire et le découplage des composants rendent notre application **extensible et réutilisable**, tout en garantissant la **maintenabilité** et la **robustesse** du système pour une adaptation à d’autres fédérations sportives.



# Diagramme de Classes :

![Diagramme de Classes](/DiagrammeClasse/DiagrammeC.jpg)

# Diagramme d'activité : 

Nous avons choisi de réaliser un diagramme d’activité représentant les différentes actions possibles pour l’utilisateur. 
Cela rend la lecture des diagrammes beaucoup plus facile. 

## Inscrire un joueur :

![Inscrire un joueur](/DiagrammeA/AddPlay.jpg) 

## Modifier un joueur :

![Modifier un joueur](/DiagrammeA/EditPlay.jpg)

## consulter un jouer :

 ![consulter un jouer](/DiagrammeA/ConsultPlay.jpg)

## supprimer un joueur : 

![supprimer un joueur](/DiagrammeA/RemovePlay.jpg)

## Inscrire une compétition :

![Inscrire une compétition](/DiagrammeA/AddComp.jpg)

## Modifier une compétition :

![Modifier une compétition](/DiagrammeA/EditComp.jpg)

## inscrire des jours à la compétition : 

![](/DiagrammeA/SubPlayComp.jpg)

## consulter une compétition : 

![consulter une compétition](/DiagrammeA/ConsultComp.jpg)

## supprimer une compétition : 

![supprimer une compétition](/DiagrammeA/)

## Inscrire une partie :

![Inscrire une partie](/DiagrammeA/AddGame.jpg)

## Modifier une partie  :

![Modifier une partie](/DiagrammeA/EditGame.jpg)

## Consulter une partie :

![Consulter une partie](/DiagrammeA/ConsultGame.jpg)

## Supprimer une partie :

![Supprimer une partie](/DiagrammeA/RemoveGame.jpg)

## Consulter le classement Elo : 

![Consulter le classement Elo](/DiagrammeA/ELO.jpg)



#  Diagramme Séquencielle : 

Tout comme pour le diagramme d'activité , nous avons choisi de réaliser un diagramme **séquenctielle** pour chaque **View Model** de notre **Application** l’utilisateur. 
Cela rend la lecture des diagrammes beaucoup plus facile.

## *AddCompetitionPageViewModel*

![AddCompetitionPageViewModel](/DiagrammeS/AddComp.jpg)

## *ConsultCompetitionPageViewModel*

![ConsultCompetitionPageViewMode](/DiagrammeS/ConsultComp.jpg)

## *EditCompetitionPageViewModel*

![EditCompetitionPageViewModel](/DiagrammeS/EditComp.jpg)

## *RemoveCompetitionPageViewModel*

![RemoveCompetitionPageViewModel](/DiagrammeS/RemoveComp.jpg)

## *SubcriptionPageVieuwModel*

![SubcriptionPageVieuwModel](/DiagrammeS/SubPlayComp.jpg)




## *AddPlayerPageViewModel*

![AddPlayerPageViewModel](/DiagrammeS/AddPlay.jpg)

## *ConsultPlayerPageViewModel*

![ConsultPlayerPageViewModel](/DiagrammeS/ConsultPlay.jpg)

## *EditPlayerPageViewModel*

![EditPlayerPageViewModel](/DiagrammeS/EditPlay.jpg)

## *RemovePlayerPageViewModel*

![RemovePlayerPageViewModel](/DiagrammeS/RemovePlay.jpg)


## *AddGamePageViewModel*

![AddGamePageViewModel](/DiagrammeS/AddGame.jpg)

## *ConsultGamePageViewModel*

![ConsultGamePageViewModel](/DiagrammeS/ConsultGame.jpg)

## *EditGamePageViewModel*

![EditGamePageViewModel](/DiagrammeS/EditGame.jpg)

## *RemoveGamePageViewModel*

![RemoveGamePageViewModel](/DiagrammeS/RemoveGame.jpg)

## *EloRankingPlayerViewModel*

![EloRankingPlayerViewModel](/DiagrammeS/Elo.jpg)

