# spaceengineers-scripts
These contain random scripts I wrote for Space Engineers. They are targeted towards the specific ships I've built, but with similar configuration should be reusable.

# Cockpit Chair Ready System

![ezgif-5-9ecb4b6dae](https://user-images.githubusercontent.com/48269287/211874238-4190b55f-7267-4c88-b874-7b4ad46d942b.gif)


The idea behind this is just to mimic the scifi, specifically Star Citizen, cockpit feature where the pilot are able to sit and the chair will swivel accordingly as well as have the LCD huds ready themselves

An unexpected challenge was making sure on repeated runs (as the script is tied to a sensor block), the script won't just run over and over in a way that will confuse the chair. This ended up being solved by adding a constraint to check minimum rotation length


# Landing Gear Automation

![Space Engineers Screenshot 2022 02 26 - 18 30 03 26](https://user-images.githubusercontent.com/48269287/211874825-c9ff4945-eaa2-438a-9e96-6c476c467a90.png)


An idea we threw around was having multiple hinges or pistons on a single landing gear, allowing them to tuck away neatly and saves space.

while it is possible to manage them all with a timer block, utilizing a script block proved to be a relatively decent practice for programming with the Space Engineers API

This script simply allows all block with a certain string to have them retract or extend simultaneously, while also keeping in my constraints such as minimum extension length.
