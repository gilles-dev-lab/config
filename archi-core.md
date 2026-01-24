/*
          _____                   _______                   _____                    _____          
         /\    \                 /::\    \                 /\    \                  /\    \         
        /::\    \               /::::\    \               /::\    \                /::\    \        
       /::::\    \             /::::::\    \             /::::\    \              /::::\    \       
      /::::::\    \           /::::::::\    \           /::::::\    \            /::::::\    \      
     /:::/\:::\    \         /:::/~~\:::\    \         /:::/\:::\    \          /:::/\:::\    \     
    /:::/  \:::\    \       /:::/    \:::\    \       /:::/__\:::\    \        /:::/__\:::\    \    
   /:::/    \:::\    \     /:::/    / \:::\    \     /::::\   \:::\    \      /::::\   \:::\    \   
  /:::/    / \:::\    \   /:::/____/   \:::\____\   /::::::\   \:::\    \    /::::::\   \:::\    \  
 /:::/    /   \:::\    \ |:::|    |     |:::|    | /:::/\:::\   \:::\____\  /:::/\:::\   \:::\    \ 
/:::/____/     \:::\____\|:::|____|     |:::|    |/:::/  \:::\   \:::|    |/:::/__\:::\   \:::\____\
\:::\    \      \::/    / \:::\    \   /:::/    / \::/   |::::\  /:::|____|\:::\   \:::\   \::/    /
 \:::\    \      \/____/   \:::\    \ /:::/    /   \/____|:::::\/:::/    /  \:::\   \:::\   \/____/ 
  \:::\    \                \:::\    /:::/    /          |:::::::::/    /    \:::\   \:::\    \     
   \:::\    \                \:::\__/:::/    /           |::|\::::/    /      \:::\   \:::\____\    
    \:::\    \                \::::::::/    /            |::| \::/____/        \:::\   \::/    /    
     \:::\    \                \::::::/    /             |::|  ~|               \:::\   \/____/     
      \:::\    \                \::::/    /              |::|   |                \:::\    \         
       \:::\____\                \::/____/               \::|   |                 \:::\____\        
        \::/    /                 ~~                      \:|   |                  \::/    /        
         \/____/                                           \|___|                   \/____/         
                                                                                                    
*/

# Architecture

## MySite.Web (ASP.NET Core MVC)
Uniquement le web (UI + orchestration)
  • • • •
Règles importantes
Pas d’accès direct à la base Pas de logique métier
  Appelle uniquement
Contient :
• Controllers
• ViewModels 
• Filtres
• Middlewares 
• Configuration

## MySite.Application
MySite.Application (logique applicative) Cœur fonctionnel du site
Ici on met :
• règles métier applicatives
• orchestration des cas d’usage • validation
• mapping DTO ↔ Domain
Aucune dépendance vers Web ou Infrastructure

## MySite.Domain (métier pur) 
Le domaine, sans dépendances
  • • • •
Règles
Pas d’Entity Framework
Pas de dépendances externes Logique métier pure
Testable facilement

## MySite.Infrastructure (technique) 
Tout ce qui est technique
 • • • •
Contient :
Entity Framework Core Repositories concrets Appels API externes Email, fichiers, cache, etc.


## Cas LR :


MySite.Application 
│
├── Orchestration
│ ├── OrchestratorBase.cs
│ ├── IDagBuilder.cs
│ ├── DagBuilder.cs
│ ├── BuildContext.cs
│ ├── IStep.cs
│ ├── StepBase.cs
│ └── GraphValidation.cs
│ └── IBuildContextToVmConverter<TViewModel>
│
├── ProductList
│ ├── ProductListOrchestrator.cs
│ ├── Steps
│ │
│ │
│ │
│ └── ProductListParameters.cs
├── LoadProductsStep.cs
├── ComputePricesStep.cs


[ Web ]
  Controller
      ↓
[ Application ]
 Orchestrator
 BuildContext
 DAG
 Steps
    ↓
[ Domain ]
 Services métier
 Interfaces
 Entités
↑
[ Infrastructure ]
Repositories
Services techniques

## Structure type (multi-sites)

MySolution.sln 
│
├── src
│ ├── SiteA.Web
│ ├── SiteB.Web
│ ├── SiteC.Web │
│ ├── MyCompany.Application
│ ├── MyCompany.Domain
│ ├── MyCompany.Infrastructure 
│
└── tests
