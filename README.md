# **�nonc�**

Bonjour et bienvenue dans l'�quipe de Merjane, le leader de la vente en ligne � A�n Seba�.

Chez Merjane, nous faisons de notre mieux chaque jour pour fournir � nos clients les produits qu'ils aiment et leur assurer une satisfaction maximale. C'est pour cela que nous gardons un �il attentif sur la disponibilit� et la qualit� de nos produits.

Malheureusement, certains produits ont des particularit�s li�es � leur disponibilit� ou � leur expiration. Et c'est l� que vous intervenez.

Notre �quipe informatique dont vous faites partie a mis en place un syst�me pour suivre notre inventaire. Il a �t� d�velopp� par Hamid, une personne pleine de bon sens qui est partie pour de nouvelles aventures.

Votre objectif est d'ajouter un nouveau type de produit � notre catalogue, un produit "FLASHSALE", et de g�rer sa disponibilit� de mani�re ad�quate.

Mais d'abord, laissez-moi vous pr�senter notre syst�me :

- Tous les produits ont une valeur **`available`** qui d�signe le nombre d'unit�s disponibles en stock.
- Tous les produits ont une valeur **`leadTime`** qui d�signe le nombre de jours n�cessaires pour le r�approvisionnement.
- � la fin de chaque commande, notre syst�me d�cr�mente la valeur **`available`** pour chaque produit command�.

Jusqu'ici, tout va bien. Mais voici o� �a se corse :

- Les produits "NORMAL" ne pr�sentent aucune particularit�. Lorsqu'ils sont en rupture de stock, un d�lai est simplement annonc� aux clients.
- Les produits "SEASONAL" ne sont disponibles qu'� certaines p�riodes de l'ann�e. Lorsqu'ils sont en rupture de stock, un d�lai est annonc� aux clients, mais si ce d�lai d�passe la saison de disponibilit�, le produit est consid�r� comme non disponible. Quand le produit est consid�r� comme non disponible, les client sont notifi�s de cette indisponibilit�. 
- Les produits "EXPIRABLE" ont une date d'expiration. Ils peuvent �tre vendus normalement tant qu'ils n'ont pas expir�, mais ne sont plus disponibles une fois la date d'expiration pass�e.
- Les produits "FLASHSALE" sont des produits en vente flash. Ils sont disponibles pour une p�riode de temps tr�s limit�e et ont une quantit� maximale d'articles pouvant �tre vendus. Une fois la p�riode de vente flash termin�e ou le nombre maximal d'articles vendus, ils ne sont plus disponibles.

Votre t�che est d'ajouter le nouveau type de produits "FLASHSALE" � notre syst�me, en prenant en compte les particularit�s de sa disponibilit�. Vous devrez �galement faire en sorte que l'ensemble du code soit facilement compr�hensible et maintenable pour le prochain d�veloppeur qui travaillera sur ce projet.


### Consignes .NET: 
* Ignorez les migrations BDD
* Ne pas modifier les classes qui ont un commentaire: `// WARN: Should not be changed during the exercise
`
* Pour lancer les tests: `dotnet test`