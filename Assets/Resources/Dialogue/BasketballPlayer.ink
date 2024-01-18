-> start


=== start ===
Hello. Are you here to play basketball with us? #speaker: Basketball Player
+ [Yes.]
-> yes
+ [No.]
-> no
+ [Was wondering of a few things.]
-> question_section

=== yes ===
Then go ask the person near the entrance. He can you put you on a team to play with us.
-> END

=== no ===
Well, alright then.
-> END

=== question_section ===
You have questions?
+ [Why do you play basketball?]
-> why_basketball
+ [Heard something about a drug dealer?]
-> drug_answer
+ [Goodbye.]
-> goodbye

=== why_basketball ===
Because it's fun! Also, it improves health and reduces stress. Life is stressful these days, so might as well find ways to destress.
-> question_section

=== drug_answer ===
Yes. Since you're a highschool student, I should tell you stay away from them.

I used to be a drug user... I nearly destroyed my life over it.

I even ended up hurting my sister and brother. They never talked to me ever since.

I... I think I'll do something else now.
-> END

=== goodbye ===
Alright, goodbye.
-> END




