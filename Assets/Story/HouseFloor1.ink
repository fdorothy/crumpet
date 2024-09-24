=== house_floor_1(_position) ===
{ update_location("HouseFloor1", _position) }
~ music = "house_theme"
-> options

= options

# clickables: clear

 + [{exit("door", "Leave")}] -> leave_house ->
 - -> options
 
= leave_house
You cannot do that
->->