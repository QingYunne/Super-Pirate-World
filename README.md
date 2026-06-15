# 🏴‍☠️ Super Pirate World

A 2D adventure/platformer game built with **Unity**. Play as a daring pirate exploring 5 levels, collecting treasures, overcoming platforming challenges, and avoiding enemies to uncover the legendary pirate treasure.

---

## 🎮 Main Features

- **Overworld map** — 5 interconnected levels to explore and unlock progressively
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

## 👤 Author

Made by **[QingYunne](https://github.com/QingYunne)** · [LinkedIn](https://linkedin.com/in/thanhvan-nguyenthi)
