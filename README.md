# 🏴‍☠️ Super Pirate World

A 2D adventure/platformer game built with **Unity**. Play as a daring pirate exploring 5 levels, collecting treasures, overcoming platforming challenges, and avoiding enemies to uncover the legendary pirate treasure.

---

## 🎮 Main Features

- **Overworld map** — 6 interconnected levels to explore and unlock progressively
- **Platformer mechanics** — movement, jumping, wall slide, wall jump, drop-through platforms
- **Enemy AI** — patrolling enemies, projectile-firing enemies, and various hazards
- **Health & collectibles system** — hearts, coins, potions, and rewards
- **Moving platforms** — helicopters, boats, palm trees, and rideable objects
- **UI** — Main Menu, In-game HUD, Game Over screen
- **AI-generated assets** — app icon and main menu UI created with [Leonardo AI](https://leonardo.ai); background music generated with [Suno](https://suno.com) — all other in-game graphics and sound effects are original assets

---

## 📥 Download & Play

The latest Windows build is available in the [Releases](https://github.com/QingYunne/Super-Pirate-World/releases/latest) section.

**Requirements:** Windows 10/11 (64-bit)

1. Download the `.zip` from the latest release.
2. Extract the folder.
3. Run the `.exe` file inside the extracted folder.

---

## 🕹️ How to Play

### 🌍 Overworld Map

| Key | Action |
|---|---|
| **↑ ↓ ← →** / **W A S D** | Move to a connected node |
| **Enter** | Enter the selected level |

- Only the first level is unlocked at the start.
- Reach the **Flag** at the end of a stage to complete the level, restore **1 Heart**, and unlock the next level on the overworld map.

### 🏴‍☠️ In-Game Controls

| Key | Action |
|---|---|
| **A / ←** | Move Left |
| **D / →** | Move Right |
| **Space** | Jump |
| **S / ↓** | Drop through platforms / get off rideable objects |
| **J** | Attack |

### 🧗 Movement Abilities

- **Wall Slide** — touch a wall mid-air to slow your fall
- **Wall Jump** — jump away from a wall to reach higher platforms
- **Drop Through** — press **S / ↓** to fall through one-way platforms, Palm Trees, Helicopters, and other rideable objects

### ❤️ Health System

- Start with **5 Hearts**
- Taking damage from enemies, projectiles, or hazards removes 1 Heart; brief invincibility follows
- Falling into **water** removes 1 Heart and sends the player back to the overworld — the level must be replayed
- Collect a **Potion** → +1 Heart
- Collect **100 Coins** → +1 Heart
- Reach the **Flag** → +1 Heart
- Health reaches 0 → Game Over

### 🪙 Items & Rewards

| Item | Effect |
|---|---|
| 🥈 Silver Coin | +1 Coin |
| 🪙 Gold Coin | +5 Coins |
| 💎 Diamond | +20 Coins |
| 💀 Skull | +50 Coins |
| 🧪 Potion | Restore 1 Heart |

### ⚠️ Enemies & Hazards

| Enemy / Hazard | Behavior |
|---|---|
| 🦷 Tooth | Patrols an area; attack reverses its direction |
| 🐚 Shell | Fires a Pearl every 3s when player is in range |
| ⚪ Pearl | Projectile; attack reverses its direction |
| 🌵 Floor Spike | Static hazard |
| 🪚 Spike | Rotates around a fixed point |
| ⚙️ Static Saw | Stationary spinning saw |
| ⚙️ Saw | Moves along a predefined path |

---

## 🛠️ Tech Stack

| Tool | Purpose |
|---|---|
| Unity 6000.0.x LTS (or compatible) | Game engine |
| C# | Game logic & scripting |
| Leonardo AI | App icon & main menu UI |
| Suno | Background music |
| Git | Source control |

---

## 🚀 Viewing the Source Code

If you want to explore the code or run the project in the Editor:

1. Clone the repository:
```bash
git clone https://github.com/QingYunne/Super-Pirate-World.git
```
2. Open the project in **Unity 6000.0.x LTS** or a compatible version.
3. Press **Play** in the Editor.

> **Note:** `Library/`, `Temp/`, and `Builds/` are excluded from the repo — Unity will regenerate them automatically. For large assets, use **Git LFS**.

---

## 📄 Documentation

This project was developed as an internship project at **Winter Wolf - IEC Games**.

A full project report (in Vietnamese) is available in the [`documentation/`](https://github.com/QingYunne/Super-Pirate-World/tree/main/documentation) folder:
- [`intership_project_report.pdf`](https://github.com/QingYunne/Super-Pirate-World/blob/main/documentation/intership_project_report.pdf) — if you can read Vietnamese, this covers the full development process in detail.

---

## 🎬 Inspiration

Inspired by the YouTube tutorial **[Pirate Platformer Game](https://youtu.be/WViyCAa6yLI?si=WZf1TdgT1RQ7JfMp)** — originally built in Python/Pygame. Super Pirate World is a reimplementation of the same game in **Unity** and **C#**, recreating the gameplay, levels, and pirate theme from scratch in a different engine.

---

## 📸 Demo Images

### Main Menu

<img width="1280" height="800" alt="image" src="https://github.com/user-attachments/assets/fdcfae77-d13d-43ea-a53d-01b68de97f27" />

### Overworld

<img width="1280" height="800" alt="image" src="https://github.com/user-attachments/assets/5e18f0f1-8dc9-484f-a8dc-d2104c4d9737" />

<img width="1082" height="649" alt="Ảnh chụp màn hình 2026-06-15 131610" src="https://github.com/user-attachments/assets/935d2048-41a5-4e67-998c-548b80b9b2e8" />

### Level Gameplay

<img width="1280" height="800" alt="Ảnh chụp màn hình 2026-06-15 120009" src="https://github.com/user-attachments/assets/ecdff7e9-bd0d-44c1-9e74-43228d32c701" />

<img width="1162" height="643" alt="image" src="https://github.com/user-attachments/assets/d29b905d-a6c1-40d1-a0e5-22532109174e" />

<img width="1156" height="661" alt="Ảnh chụp màn hình 2026-06-15 150501" src="https://github.com/user-attachments/assets/7cf39147-616e-4dcd-aa1e-1cb285d21a1f" />


<img width="1157" height="650" alt="image" src="https://github.com/user-attachments/assets/4ddf8a50-53ac-49c7-aeef-70c8abee9559" />


<img width="1166" height="653" alt="image" src="https://github.com/user-attachments/assets/7b50a126-8137-43cb-a9f1-302f32043b9a" />

### Flag


<img width="67" height="128" alt="image" src="https://github.com/user-attachments/assets/280d78bb-4880-4986-ba4d-a09a8d4d44e2" />

### Final Tresure

<img width="203" height="176" alt="Ảnh chụp màn hình 2026-06-15 150927" src="https://github.com/user-attachments/assets/7a69a0c8-e3c6-498a-ad94-ba1fa0cd1df4" />

<img width="203" height="230" alt="Ảnh chụp màn hình 2026-06-15 150942" src="https://github.com/user-attachments/assets/620e881e-8d1f-47e8-b84e-180de8419e0f" />

---

## 👤 Author

Made by **[QingYunne](https://github.com/QingYunne)** · [LinkedIn](https://linkedin.com/in/thanhvan-nguyenthi)
