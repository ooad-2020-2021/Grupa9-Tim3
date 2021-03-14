# Grupa9-Tim3

# Parking

Ova aplikacija sluzi za ubrzavanje i olakšavanje korištenja usluga parkinga. Neke od osnovnih funkcija ove aplikacije su:
- online provjera dostupnosti mjesta i pozicije tih mjesta
- navođenja korisnika do njegovog mjesta
- zakupljenje mjesta na određeni broj dana
- popusti za korisnike koji zadovoljavaju određene uvjete

Aplikacija je napravljena kako za parkinge na kojima se nalaze iskljucivo manji automobili, tako i za parkinge sa kamp-kućicama i kamionima. Također, aplikaciju mogu koristiti i uposlenici da bi mogli uputiti do slobodnog mjesta (ukoliko takvog ima) korisnike koji nisu rezervisali mjesto za parking online putem. To se zasniva na ideji da je parking podijeljen u spratove, redove i kolone, a da uposlenik u svakom trenutku može dobiti informaciju gdje ima slobodno mjesto (npr. 2.sprat, 1. red, 3. kolona). Popuste mogu dobiti stalni korisnici, osobe s invaliditetom ili korisnici koji osvoje popust igrajući igricu "Parking" (ime nije konacno).    


online provjera ima li mjesta
online provjera gdje ima mjesta - na krajevima celija (skup auta jedno do drugog - ove jedinice u prikazu parkinga)
	 ili u sredini celije mozda (mozda graphical)
imaju kamioni
imaju kamp-kucice
imaju mjesecne karte i godisnje karte
zakupljenje mjesta, odnosno rezervirsanje (barem za kamp-kucice)
ima jedan na rampi, jedan ili vise pomazu pri parkiranju, ima strazar kad njih nema
stalni prikaz broja slobodnih mjesta (kako za kamione, tako za kamp-kucice, a tako i za auta)
popusti za stalne korisnike
mjesta za osobe sa invaliditetom (parking u pola cijene)

uprava plate, radnog vremena, obracun plate (u zavisnosti 
	od uradjenog posla tog mjeseca - prekovremeno se duplo naplacuje)

javlja se vlasniku kada treba prosiriti dio parkinga (ukoliko uzastopice vise dana ima neki dio parkinga popunjen)
onom ko ulazi daje se prvi (najmanji) dostupan broj, kada izlazi unesu 
	se podaci prvo o tom broju, pa o tome koliko je puta bio na parkingu te sedmice (ako je 4 ili vise dana popust 30% za sljedecu sedmicu)

imaju dvije rampe - jedna za kamione, 
	jedna za auta i kamp-kucice (ukoliko se pokusa kamion "unijeti" u sistem, izuzetak)

razlicite cijene u zavisnosti od doba dana (navecer jeftinije)
poslije odredjenog sata nema ulaza na parking ali ima izlaza (strazar cuva vozila tad)

mozda navodjene korisnika do njegovog parking 
	mjesta na aplikaciji - naravno ako ima aplikaciju, inace samo broj pa nek se snadje sam
	(lijevo, pravo, pravo, desno, pravo, lijevo, parking na desno) - ne nuzno najkraci put, nego se ide 
	do kraja sjeverno ili juzno, odnosono po redovima matrice se nadje prvo red pa se ide po kolonama



TREBA RAZMISLITI STA SE MOZE OD OVOG IMPLEMENTIRATI  
(treba posloziti malo ove zahtjeve)


igrica dok neko ceka, parkiranje auta, prazna mjesta ima svoje boje, auta koja trebaju biti
	parkirana imaju svoje boje i treba dovesti auta na parking mjesta sa odgovarajucim bojama
	,auta mogu ici gore-dolje, lijevo-desno, mjeri se vrijeme
