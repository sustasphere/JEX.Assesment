# JEX Assessment
Laat ik maar meteen beginnen met het slechte nieuws: ik heb niet alles af kunnen krijgen; excuses.

Redenen:
- wellicht werk ik te traag ;)
- ik heb behoorlijk veel tijd gestoken in het fiksen van een FK-issue waar ik tegen aan liep...

Wat betreft het laatste issue; ik heb zowel 'by convention', als ook 'by configuration' geprobeerd FK's aan te leggen op CompanySet. Uiteindelijk heb ik hier maar voor een minder elegante oplossing gekozen...

Daarnaast heb ik een simpele 'producer => consumer' opzet neergezet voor het genereren van objecten. Dit heb ik gedaan als voorbeeld hoe in principe alle 'requests => responses' geïmplementeerd kunnen worden. Dit komt uiteraard de SoC (Separation of Concerns) te goede. Maar hiervoor zou ik dan - naast de API als BFF - uiteraard wel een micro-service hebben moeten bouwen; en dat kost dan weer extra tijd...

Hopelijk kunnen jullie de code die ik wel heb gerealiseerd waarderen; graag bespreek ik de opzet nog in een persoonlijk gesprek.

Giovanni Scheepers