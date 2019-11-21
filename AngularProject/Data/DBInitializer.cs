using AngularProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProject.Data
{
    public class DBInitializer
    {
        public static void Initialize(ProjectContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();

            // Look for any albums.
            if (context.Stemmen.Any())
            {
                return;   // DB has been seeded
            }

            var gebruikers = new List<Gebruiker>
            {
                new Gebruiker{Email="r0695641@student.thomasmore.be", Password="amber0000"},
                new Gebruiker{Email="jos@hotmail.com", Password="jos0000"},
                new Gebruiker{Email="bart@hotmail.com", Password="bart0000"},
                new Gebruiker{Email="jade@hotmail.com", Password="jade0000"},
                new Gebruiker{Email="kenzoo@hotmail.com", Password="kenzo0000"},
                new Gebruiker{Email="jelle@hotmail.com", Password="jelle0000"},
                new Gebruiker{Email="wesley@hotmail.com", Password="wesley0000"},
                new Gebruiker{Email="linda@hotmail.com", Password="linda0000"},
            };

            new List<Vriend>
            {
                new Vriend{Gebruiker1=gebruikers.Single(g=>g.Email == "r0695641@student.thomasmore.be"), Gebruiker2=gebruikers.Single(g=>g.Email == "jos@hotmail.com"), Geaccepteerd=true},
                new Vriend{Gebruiker1=gebruikers.Single(g=>g.Email == "r0695641@student.thomasmore.be"), Gebruiker2=gebruikers.Single(g=>g.Email == "bart@hotmail.com"), Geaccepteerd=true},
                new Vriend{Gebruiker1=gebruikers.Single(g=>g.Email == "r0695641@student.thomasmore.be"), Gebruiker2=gebruikers.Single(g=>g.Email == "kenzoo@hotmail.com"), Geaccepteerd=true},
                new Vriend{Gebruiker1=gebruikers.Single(g=>g.Email == "r0695641@student.thomasmore.be"), Gebruiker2=gebruikers.Single(g=>g.Email == "jade@hotmail.com"), Geaccepteerd=true},
                new Vriend{Gebruiker1=gebruikers.Single(g=>g.Email == "kenzoo@hotmail.com"), Gebruiker2=gebruikers.Single(g=>g.Email == "jade@hotmail.com"), Geaccepteerd=true},
                new Vriend{Gebruiker1=gebruikers.Single(g=>g.Email == "jelle@hotmail.com"), Gebruiker2=gebruikers.Single(g=>g.Email == "wesley@hotmail.com"), Geaccepteerd=true},
                new Vriend{Gebruiker1=gebruikers.Single(g=>g.Email == "r0695641@student.thomasmore.be"), Gebruiker2=gebruikers.Single(g=>g.Email == "jelle@hotmail.com"), Geaccepteerd=true},
                new Vriend{Gebruiker1=gebruikers.Single(g=>g.Email == "r0695641@student.thomasmore.be"), Gebruiker2=gebruikers.Single(g=>g.Email == "wesley@hotmail.com"), Geaccepteerd=true},
                new Vriend{Gebruiker1=gebruikers.Single(g=>g.Email == "r0695641@student.thomasmore.be"), Gebruiker2=gebruikers.Single(g=>g.Email == "linda@hotmail.com"), Geaccepteerd=true},
                new Vriend{Gebruiker1=gebruikers.Single(g=>g.Email == "jade@hotmail.com"), Gebruiker2=gebruikers.Single(g=>g.Email == "linda@hotmail.com"), Geaccepteerd=true},
                new Vriend{Gebruiker1=gebruikers.Single(g=>g.Email == "jos@hotmail.com"), Gebruiker2=gebruikers.Single(g=>g.Email == "jade@hotmail.com"), Geaccepteerd=true},
                new Vriend{Gebruiker1=gebruikers.Single(g=>g.Email == "jos@hotmail.com"), Gebruiker2=gebruikers.Single(g=>g.Email == "jelle@hotmail.com"), Geaccepteerd=true},
                new Vriend{Gebruiker1=gebruikers.Single(g=>g.Email == "jos@hotmail.com"), Gebruiker2=gebruikers.Single(g=>g.Email == "wesley@hotmail.com"), Geaccepteerd=true},
                new Vriend{Gebruiker1=gebruikers.Single(g=>g.Email == "jos@hotmail.com"), Gebruiker2=gebruikers.Single(g=>g.Email == "linda@hotmail.com"), Geaccepteerd=false},
                new Vriend{Gebruiker1=gebruikers.Single(g=>g.Email == "jos@hotmail.com"), Gebruiker2=gebruikers.Single(g=>g.Email == "kenzoo@hotmail.com"), Geaccepteerd=false},
            }.ForEach(v => context.Vrienden.Add(v));

            var polls = new List<Poll>
            {
                new Poll{Titel="Waar gaan we eten?"},
                new Poll{Titel="Wat gaan we doen?"}
            };

            var pollGebruikers = new List<PollGebruiker> { 
                new PollGebruiker { GebruikerId = gebruikers.Single(g => g.Email == "r0695641@student.thomasmore.be").GebruikerId, PollId = polls.Single(p => p.Titel == "Waar gaan we eten?").PollId, HeeftAangemaakt = true , HeeftGeaccepteerd=true},
                new PollGebruiker { Gebruiker = gebruikers.Single(g => g.Email == "jos@hotmail.com"), Poll = polls.Single(p => p.Titel == "Waar gaan we eten?"), HeeftAangemaakt = false, HeeftGeaccepteerd=false },
                new PollGebruiker { Gebruiker = gebruikers.Single(g => g.Email == "bart@hotmail.com"), Poll = polls.Single(p => p.Titel == "Waar gaan we eten?"), HeeftAangemaakt = false, HeeftGeaccepteerd=true },
                new PollGebruiker { Gebruiker = gebruikers.Single(g => g.Email == "jelle@hotmail.com"), Poll = polls.Single(p => p.Titel == "Waar gaan we eten?"), HeeftAangemaakt = false, HeeftGeaccepteerd=true },
                new PollGebruiker { Gebruiker = gebruikers.Single(g => g.Email == "wesley@hotmail.com"), Poll = polls.Single(p => p.Titel == "Waar gaan we eten?"), HeeftAangemaakt = false, HeeftGeaccepteerd=true },
                new PollGebruiker { Gebruiker = gebruikers.Single(g => g.Email == "linda@hotmail.com"), Poll = polls.Single(p => p.Titel == "Waar gaan we eten?"), HeeftAangemaakt = false, HeeftGeaccepteerd=true },
                new PollGebruiker { Gebruiker = gebruikers.Single(g => g.Email == "r0695641@student.thomasmore.be"), Poll = polls.Single(p => p.Titel == "Wat gaan we doen?"), HeeftAangemaakt = false, HeeftGeaccepteerd=true },
                new PollGebruiker { Gebruiker = gebruikers.Single(g => g.Email == "jade@hotmail.com"), Poll = polls.Single(p => p.Titel == "Wat gaan we doen?"), HeeftAangemaakt = false, HeeftGeaccepteerd=true },
                new PollGebruiker { Gebruiker = gebruikers.Single(g => g.Email == "bart@hotmail.com"), Poll = polls.Single(p => p.Titel == "Wat gaan we doen?"), HeeftAangemaakt = false, HeeftGeaccepteerd=true },
                new PollGebruiker { Gebruiker = gebruikers.Single(g => g.Email == "jos@hotmail.com"), Poll = polls.Single(p => p.Titel == "Wat gaan we doen?"), HeeftAangemaakt = true, HeeftGeaccepteerd=true }

            };

            var keuzes = new List<Keuze>
            {
                new Keuze{Naam="Frituur", Poll=polls.Single(p => p.Titel=="Waar gaan we eten?")},
                new Keuze{Naam="Pizzeria", Poll=polls.Single(p => p.Titel=="Waar gaan we eten?")},
                new Keuze{Naam="Kebab", Poll=polls.Single(p => p.Titel=="Waar gaan we eten?")},
                new Keuze{Naam="Zelf koken", Poll=polls.Single(p => p.Titel=="Waar gaan we eten?")},
                new Keuze{Naam="Poolen", Poll=polls.Single(p => p.Titel=="Wat gaan we doen?")},
                new Keuze{Naam="Drinken", Poll=polls.Single(p => p.Titel=="Wat gaan we doen?")},
                new Keuze{Naam="Film kijken", Poll=polls.Single(p => p.Titel=="Wat gaan we doen?")},
            };

            new List<Stem>
            {
                new Stem{Gebruiker=gebruikers.Single(g=>g.Email == "r0695641@student.thomasmore.be"), Keuze=keuzes.Single(k => k.Naam=="Pizzeria")},
                new Stem{Gebruiker=gebruikers.Single(g=>g.Email == "jos@hotmail.com"), Keuze=keuzes.Single(k => k.Naam=="Pizzeria")},
                new Stem{Gebruiker=gebruikers.Single(g=>g.Email == "bart@hotmail.com"), Keuze=keuzes.Single(k => k.Naam=="Frituur")},
                new Stem{Gebruiker=gebruikers.Single(g=>g.Email == "jelle@hotmail.com"), Keuze=keuzes.Single(k => k.Naam=="Kebab")},
                new Stem{Gebruiker=gebruikers.Single(g=>g.Email == "wesley@hotmail.com"), Keuze=keuzes.Single(k => k.Naam=="Kebab")},
                new Stem{Gebruiker=gebruikers.Single(g=>g.Email == "linda@hotmail.com"), Keuze=keuzes.Single(k => k.Naam=="Zelf koken")},
                new Stem{Gebruiker=gebruikers.Single(g=>g.Email == "r0695641@student.thomasmore.be"), Keuze=keuzes.Single(k => k.Naam=="Poolen")},
                new Stem{Gebruiker=gebruikers.Single(g=>g.Email == "jade@hotmail.com"), Keuze=keuzes.Single(k => k.Naam=="Drinken")},
                new Stem{Gebruiker=gebruikers.Single(g=>g.Email == "bart@hotmail.com"), Keuze=keuzes.Single(k => k.Naam=="Film kijken")},
            }.ForEach(s => context.Stemmen.Add(s));

            context.SaveChanges();
        }
    }
}
