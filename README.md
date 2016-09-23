# AS-Backup
Gestione dei file del BSP, il programma crea un backup dei file eDreams e Opodo che arrivano dalla proprinter in Spagna e li rinomina salvandoli nelle relative cartelle

All'interno della repo si trova il file di configurazione app.config:
sourceFoldereDreams: percorso file sorgenti di eDreams
destinationFoldereDreams: percorso di destinazione dove verranno salvati i file rinominati di eDreams
filtereDreams: estensione file eDreams (es. value="DGEN-*.txt")
prefixeDreams: prefisso che viene aggiunto al momento di rinominare i file di eDreams
sourceFolderOpodo: percorso file sorgenti di Opodo
destinationFolderOpodo: percorso di destinazione dove verranno salvati i file rinominati di Opodo
filterOpodo: estensione file Opodo (es. value="*.txt")
prefixOpodo: prefisso che viene aggiunto al momento di rinominare i file di Opodo\r\n
pathLogs: percorso dove vengono salvati i log relativi al funzionamento del programma
