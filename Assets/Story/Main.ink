INCLUDE HouseFloor1.ink

VAR scene = ""
VAR position = ""
VAR interactive = false
VAR DEBUG = false
VAR music = ""
VAR holding = ""

// global game variables

<- intro

=== intro ===
This is the story of a dog.
-> house_floor_1("FromOutside")

=== win ===
THE END
:scene Death Anywhere
-> DONE

== function obj(text)
<b>{text}</b>

== function update_location(_scene, _position)

{ position != _position || _scene != scene :
  ~ scene = _scene
  ~ position = _position
  { interactive: :scene {scene} {position} }
  { debug("You are in the { scene }.") }
}

== function sfx(name)
{ interactive: :sfx {name} }

== function debug(text)

{DEBUG: > text}

== function opt(type, object, text)

{ interactive:
  ~ return ":{type} {object} {text}"
  - else: ~ return "{opt_name(type)} {text} ({object})"
}

== function opt_name(type)

{ type:
  - "i": ~ return "Investigate"
  - "e": ~ return "Exit"
}

== function investigate(object, text)

~ return opt("i", object, text)

== function exit(object, text)

~ return opt("e", object, text)

== function take(item)

~ holding = item