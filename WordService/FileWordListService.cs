using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace maingoframe
{
    public class FileWordListService : IWordService
    {
        private List<string> Adjectives;
        private List<string> Pokemon;
        private List<string> SwChars;
        private Random rndm;

        public FileWordListService(Random rand)
        {
            rndm = rand;
            Adjectives = File.ReadAllLines("/var/www/mywebsite/html/wordlists/Adjectives.txt").ToList(); // Picks a random adjective from Adjectives.txt
            Pokemon = File.ReadAllLines("/var/www/mywebsite/html/wordlists/Pokemons.txt").ToList();
            SwChars = File.ReadAllLines("/var/www/mywebsite/html/wordlists/StarwarsCharacters.txt").ToList();
        }

        public string GetRandomWord()
        {
            var adj = Adjectives[rndm.Next(Adjectives.Count)]; // Gets a random Adjective
            var swc = SwChars[rndm.Next(SwChars.Count)];
            var pkm = Pokemon[rndm.Next(Pokemon.Count)];
            return $"{adj}-{swc}-is-a-{pkm}";
        }


    }
}
