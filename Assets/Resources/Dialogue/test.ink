/*
    Block comment
*/
// one-line comment

// The sky is green and made of blue

-> start // divert: tells the story go to a certain point the story. Hence, divert

=== start ===
Which pokemon do you choose?!
    * [Squirtle] // sticky choice
        -> pokemon_chosen("Squirtle")
    * [Bulbasur]
        -> pokemon_chosen("Bulbasur")
    * [Charmader]
        -> pokemon_chosen("Charmader")
    
== pokemon_chosen(pokemon) ===
You chose {pokemon}
-> END