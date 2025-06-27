# Crew & Chaos (Working Title)

**Crew & Chaos** is a cooperative, physics-based sailing game for mobile devices (Android/iOS), designed to be played in local multiplayer with 2-4 players. Players take on different roles aboard a sailboat, each with unique tasks that require communication, coordination, and timing all in real time.

> **Voice only, no in-game chat. Just teamwork. Just chaos.**

---

##  Game Concept

- Local coop game where each player controls a specific role (Helmsman, Sail Trimmer, Lookout, Navigator).
- Every player sees the full boat from a third-person perspective.
- Players interact with physical controls sliders, wheels, ropes that respond to real-time boat physics.
- Voice communication only (just like in the real world).
- Designed for fun chaos, coordination, and shouting across the couch.

---

##  Tech Stack

- **Engine**: Unity
- **Networking**: Unity Netcode for GameObjects (NGO)
- **Platform**: Android & iOS (local Wi-Fi multiplayer)
- **Perspective**: Third-person shared view
- **Core systems**: Physics interactions, networked controls, role-based UI overlays

---

##  Gameplay Roles

| Role        | Responsibilities                                      |
|-------------|-------------------------------------------------------|
| Helmsman    | Steer the boat using a virtual wheel (circular drag) |
| Sail Trimmer| Adjust sails via rope-pulling mechanics               |
| Lookout     | Warn of obstacles, only they see water hazards       |
| Navigator   | Monitor wind direction and optimal routes            |

 Ropes must be dragged and knotted  
 Players must duck when the boom swings  
 Communication is 100% voice no buttons, no chat

---

##  How It Works

- Players select their role in a **lobby scene** (1 role per player).
- Each role has a different control overlay and tasks.
- In future versions: mid-game **role switching** via player agreement.
- Game modes may include races, storms, rescue missions.

---

##  Documents

-  [Game Design Document (English)](./docs/crew_and_chaos_gdd_en.docx)
-  [Game Design Document (German)](./docs/crew_and_chaos_gdd_de.docx)

---

## Want to Contribute?

We're looking for people excited by:
- Unity gameplay programming (physics, UI, input)
- Multiplayer dev (NGO, mobile LAN)
- Game designers with UX or sailing experience
- Artists (2D/3D) for UI mockups and boat parts

 **Get started:**
1. Clone the repo
2. Check open issues
3. Introduce yourself via Discussion or PR

---

##  Contribution Guidelines

- Be respectful and collaborative
- Open an issue before submitting major changes
- Commit early, commit often
- Speak up in Discussions!

---

##  License

This project will be open-sourced under the **MIT License**.  
Feel free to fork, learn, or contribute 鈥� just credit the team.

---

> Made by sailing nerds, for co-op chaos lovers
