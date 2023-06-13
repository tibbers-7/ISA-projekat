# ISA-projekat
POKRETANJE PROJEKTA:

Za backend je koriscena .NET tehologija, a za frontend Angular sa bazom u PostgreSQL

Baza se pravi u pgAdminu sa username 'postgres' i sifrom 'ftn'
Migracije se nalaze u sklopu backenda, tako da se tabele automatski prave
Backend API se pokrece u Visual Studio preko IISExpress
Frontend se pokrece preko terminala koristeci komandu ng serve (npm mora biti instaliran)


LOGOVANJE:

Admin:
    email:admin
    password:admin
    
Donor:
    email:donor
    password:donor
 
Staff:
    email:staff
    password:staff
    
Izabrane konfliktne situacije: 
    1.administrator centra ne može unapred definisati termin u isto ili
preklapajuće vreme za koje i korisnik kreira rezervaciju termina,
    2. više istovremenih korisnika aplikacije ne može da rezerviše termin koji je u
međuvremenu postao nedostupan
