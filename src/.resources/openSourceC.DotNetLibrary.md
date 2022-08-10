##### Generate a new key pair strong-named key file:

`sn -k openSourceC.DotNetLibrary.snk`

##### Extract the public key from the strong-named key file to a separate file:

`sn -p openSourceC.DotNetLibrary.snk openSourceC.DotNetLibrary.pkey`

##### Display the full public key to the console:

`sn -tp openSourceC.DotNetLibrary.pkey > openSourceC.DotNetLibrary.txt`


##### Public key (hash algorithm: sha1):
0024000004800000940000000602000000240000525341310004000001000100079f4397a282c617ccbf75ff076b02f59981092a42fc45262950c4a5a032add635a5332bd884365fdb086d0b357aa193e3177a991c29b9bdc4befeac643673c990a656d4b650200ee6d5a41e19389fee781682ac6ebb9241e1aaaf5da45a15640bb693c172f2bf1a54196d4cdb045f4ab8e9922cea6068c0500e5147c64e2e99

##### Public key token:
ffe42b835e51f6fa