# KickBlast Judo – Training Fee Management System
## Premium Windows Desktop UI/UX Design Specification (Handoff Document)

---

## 1) Design System

### 1.1 Brand and Visual Direction
A premium admin-style interface designed for a Sri Lankan sports training centre: confident, clean, athletic, and trustworthy. The visual language combines deep navy surfaces with energetic accent highlights to communicate discipline, performance, and professionalism.

### 1.2 Color System

#### Primary Palette (Sports + Professional)
- **Primary 900 (Base):** `#0B1F3A` (Deep Navy)
- **Primary 700:** `#163B68` (Structured Blue)
- **Primary 500:** `#1F5FA8` (Core Brand Blue)
- **Primary 300:** `#5D8FC7` (Soft Highlight Blue)

#### Accent
- **Accent 500:** `#F5A623` (Premium Amber)
- **Accent 300:** `#FFD27A` (Light Accent for chips/badges)

#### Background & Surfaces
- **App Background:** `#F4F7FB`
- **Surface Primary (Cards):** `#FFFFFF`
- **Surface Secondary (Raised sections):** `#F9FBFE`
- **Sidebar Background:** `#0F2747`
- **Header Background:** `#FFFFFF`

#### Text Colors
- **Text Primary:** `#15243A`
- **Text Secondary:** `#4A5D75`
- **Text Muted:** `#7A889B`
- **Text on Dark:** `#F3F7FD`

#### Semantic Colors
- **Success:** `#1F9D55`
- **Warning:** `#D9831F`
- **Error:** `#D64545`
- **Info:** `#2F80ED`

#### Borders & Dividers
- **Border Default:** `#DCE4EF`
- **Border Strong:** `#C9D4E2`
- **Divider Light:** `#E8EEF6`

### 1.3 Typography (Segoe UI)

#### Font Family
- **Primary Font:** Segoe UI
- **Fallback:** "Segoe UI", "Arial", sans-serif

#### Type Scale
- **H1:** 32px, Semibold, line-height 40px
- **H2:** 24px, Semibold, line-height 32px
- **H3:** 18px, Semibold, line-height 26px
- **Body Large:** 15px, Regular, line-height 22px
- **Body:** 14px, Regular, line-height 20px
- **Small:** 12px, Regular, line-height 18px
- **Label/Overline:** 11px, Semibold, line-height 16px, letter spacing 0.4px

#### Numeric Emphasis
- KPI and currency values should use **Semibold** weight with slightly tighter tracking (-0.2px) for visual precision.

### 1.4 Spacing, Radius, and Elevation
- **Global app padding:** 24px
- **Section gap:** 20px
- **Card padding:** 20px
- **Input horizontal padding:** 12px
- **Input vertical padding:** 10px
- **Card border radius:** 14px
- **Input border radius:** 10px
- **Button border radius:** 10px
- **Chip border radius:** 999px (pill)

#### Shadow Style
- **Card shadow:** `0 6px 18px rgba(16, 36, 64, 0.08)`
- **Hover shadow:** `0 10px 24px rgba(16, 36, 64, 0.14)`
- **Drawer shadow:** `-8px 0 28px rgba(10, 24, 45, 0.18)`

---

## 2) Global Layout Structure

### 2.1 Shell Layout
Desktop-first composition using a fixed sidebar and fluid content.

- **Left Sidebar (fixed):** 248px width
- **Top Header:** 72px height
- **Main Content Area:** fills remaining width/height
- **Content max width (inside main):** 1440px
- **Layout style:** cards-based with consistent spacing rhythm

### 2.2 Sidebar Navigation

#### Menu Items (fixed order)
1. Dashboard
2. Athletes
3. Fee Calculator
4. History
5. Settings

#### Navigation Item Anatomy
- Left-aligned icon (18px)
- 10px gap icon-to-label
- Label text: 14px Semibold
- Item height: 42px
- Horizontal padding: 14px

#### Active State
- Background: `rgba(245, 166, 35, 0.16)`
- Left indicator bar: 3px Accent 500
- Text/icon color: Accent 300 on dark sidebar
- Subtle inner glow for premium depth

#### Hover State
- Background: `rgba(255, 255, 255, 0.08)`
- Text/icon slightly brightened
- Cursor: pointer

#### Animation Behavior
- Hover transition: 140ms ease-out
- Active indicator slide-in: 180ms ease-out
- Page route transition triggered from sidebar: 220ms fade+slide

### 2.3 Top Header Bar
Header includes:
- **Application title:** “KickBlast Judo – Training Fee Management System”
- **Current date (Sri Lankan format):** e.g., “17 Feb 2026”
- **Optional theme toggle:** Light / Dark mode switch

Header layout:
- Left: app title + optional subtitle (“Admin Console”)
- Right: date badge + theme toggle
- Bottom divider: 1px `#E8EEF6`

---

## 3) Screen-by-Screen UI Design

## A) Dashboard Page

### A.1 Page Header
- **Greeting title (H2):** “Welcome back, Admin”
- **Subtext (Body):** “Here is your training fee overview for this month.”

### A.2 KPI Summary Cards (4 cards, equal width)
Grid: 4 columns on large desktop, wrap to 2x2 on narrower windows.

#### Card Structure (each)
- Top row: icon container (36x36), muted label text
- Middle row: main value (H2 style)
- Bottom row: optional trend/context text (Small)

#### KPI Cards
1. **Total Athletes**
   - Icon: user group
   - Value style: 30px Semibold
2. **Calculations This Month**
   - Icon: calculator
   - Value style: 30px Semibold
3. **Total Revenue (LKR)**
   - Icon: wallet/rupee
   - Value style: 30px Semibold
   - Format example: `LKR 245,000.00`
4. **Next Competition Date (Second Saturday)**
   - Icon: calendar
   - Value style: 22px Semibold
   - Date format example: `14 Mar 2026`

### A.3 Recent Calculations Table (latest 5)
Card title: “Recent Calculations”

Columns:
- Date
- Athlete Name
- Plan
- Competitions
- Total Fee
- Status

Table behavior:
- Sticky header inside card
- Row hover highlight: `#F5F9FF`
- Zebra optional subtle striping (very low contrast)

### A.4 Quick Actions
Buttons aligned right of section header or below KPI cards:
- “Add Athlete” (secondary)
- “New Fee Calculation” (primary)
- “View Full History” (ghost)

---

## B) Athletes Page

### B.1 Toolbar
- Search bar (left, 320px min width)
  - Placeholder: “Search by athlete name, phone, or ID”
- Plan filter chips (right):
  - All, Beginner, Intermediate, Elite

Chip styles:
- Default: outlined, muted text
- Active: filled Accent 500, white text
- Hover: subtle tint change

### B.2 Data Grid
Columns:
1. Athlete ID
2. Full Name
3. Age
4. Weight (kg)
5. Current Plan
6. Join Date
7. Last Payment Date
8. Status
9. Actions

Grid design:
- Header: 13px Semibold, uppercase-ish tracking
- Rows: 46px height
- Row hover: background `#F7FAFF`
- Selected row: left accent border + tinted background

Actions column:
- Edit icon button (pencil)
- Delete icon button (bin)
- Icon buttons are 30x30 with tooltip on hover

### B.3 Add/Edit Athlete Drawer (Right-side sliding panel)
- Width: 420px
- Entrance: slide from right + fade (220ms)
- Header: form title + close icon

Field layout (single column, grouped):
- Personal Info: Full Name, Athlete ID, Age
- Physical Info: Weight (kg), Target Weight (kg)
- Enrollment Info: Plan, Join Date, Contact Number

Inline validation:
- Invalid field border: Error color
- Error text below field: 12px, Error color
- Validation icon inside field optional

Buttons (sticky footer in drawer):
- Primary: “Save Athlete”
- Secondary: “Cancel”

### B.4 Error & Confirmation UX
- **Error message appearance:** compact red inline text, appears under field within 120ms
- **Delete confirmation dialog:** centered modal
  - Title: “Delete athlete record?”
  - Body: clear warning + irreversible note
  - Actions: “Delete” (danger), “Cancel” (neutral)

---

## C) Fee Calculator Page

### C.1 Layout
Two-column layout with 60/40 split.
- **Left (input workflow):** form card stack
- **Right (output):** sticky invoice preview card

### C.2 Left Column Inputs
1. **Athlete selector** (searchable dropdown)
2. **Competitions input** (numeric stepper)
3. **Coaching hours input** (numeric, range 0–5)
4. **Validation notes** (helper text region)
5. **Primary CTA:** “Calculate Fee”

Validation behaviors:
- If coaching hours > 5: show blocking inline error
- If required athlete not selected: disable primary button

### C.3 Right Column Invoice Preview
Card title: “Fee Breakdown”

Invoice metadata:
- Athlete Name
- Plan
- Calculation Date (e.g., `17 Feb 2026`)
- Next Competition (Second Saturday)

Itemised costs:
- Training Cost
- Coaching Cost
- Competition Cost
- Divider
- **TOTAL** (bold, larger, accent underline)

Currency format (strict):
- `LKR 00,000.00`
- Examples: `LKR 12,500.00`, `LKR 245,000.00`

### C.4 Weight Comparison Badge
Dynamic badge near athlete summary:
- **Over by X kg** (warning orange)
- **Under by X kg** (info blue)
- **On target** (success green)

Badge style:
- Pill chip, icon + text
- 12px text, Semibold

### C.5 Second Saturday Display
Prominent date chip:
- Label: “Second Saturday”
- Value: formatted date (e.g., `14 Mar 2026`)
- Placement: invoice header right

---

## D) History Page

### D.1 Filter Row
Inline filter controls at top:
- Athlete dropdown
- Month dropdown
- Year dropdown
- Reset filters action

Control styling:
- Compact outlined inputs
- Clear labels above each field

### D.2 History Data Grid
Columns:
1. Calculation ID
2. Date
3. Athlete Name
4. Plan
5. Competitions
6. Coaching Hours
7. Total Amount (LKR)
8. Performed By
9. Actions (view details)

Sorting and interaction:
- Sortable columns with chevron indicator
- Row click opens detail preview panel

### D.3 Detail Preview Panel
Right-side contextual panel (not full drawer):
- Width: 360px
- Shows selected calculation summary
- Includes line-item breakdown and note field

### D.4 Empty State
When no records found:
- Illustration: minimalist clipboard/calendar graphic
- Title: “No history records found”
- Body: “Try adjusting your filters or create a new fee calculation.”
- CTA: “Go to Fee Calculator”

---

## E) Settings Page

### E.1 Layout
Vertical stack of cards with clear section grouping:
1. Pricing Configuration
2. Theme & Appearance
3. Data & Safety

### E.2 Pricing Card (LKR fields)
Fields:
- Base Training Fee
- Coaching Fee (per hour)
- Competition Fee (per event)
- Late Fee (optional)

Input behavior:
- Currency prefix displayed: `LKR`
- Formatted display while editing/blur: `LKR 00,000.00`

### E.3 Theme & Accent
- Theme toggle: Light / Dark
- Accent swatches (selectable): Amber (default), Teal, Royal Blue
- Selected swatch gets ring and checkmark

### E.4 Reset Database Warning Card
High-emphasis warning section:
- Background tint: very light red
- Left icon: warning triangle
- Message: irreversible action warning
- Button: “Reset Database” (danger style)

### E.5 Actions
Bottom sticky action row:
- “Save Changes” (primary filled)
- “Discard” (secondary)
- Danger action isolated from primary actions for safety

---

## 4) Localization Rules (Sri Lanka)

### 4.1 Currency Formatting
All monetary values must follow:
- **Format:** `LKR 12,500.00`
- Thousands separator: comma
- Decimal precision: 2
- Prefix: `LKR` + space

### 4.2 Date Formatting
All user-visible dates must follow:
- **Format:** `17 Feb 2026`
- Day as two-digit where needed, month short text, four-digit year

### 4.3 Language and Tone
- English (Sri Lankan professional tone)
- Clear, concise admin wording
- Avoid slang and regionally ambiguous terms

---

## 5) Micro Interactions

### 5.1 Button Hover Animation
- TranslateY: -1px
- Shadow increase from base to hover shadow
- Duration: 140ms ease-out

### 5.2 Page Transition Animation
- Enter: 12px upward fade-in
- Exit: light fade-out
- Total route transition: 200–240ms

### 5.3 Snackbar / Toast Notifications
Position:
- Bottom-right stack (desktop)

Variants:
- Success (green accent)
- Warning (amber accent)
- Error (red accent)

Behavior:
- Auto-dismiss 3.5s
- Pause on hover
- Includes icon + short action link (optional)

### 5.4 Loading Indicators
- Primary: skeleton loaders for cards/tables
- Secondary: inline spinner in buttons when processing
- Keep layout stable to avoid content shift

### 5.5 Empty State Illustrations
Use lightweight monochrome illustrations with brand accent highlights.
Each empty state includes:
- Clear title
- One-sentence guidance
- Single primary CTA

---

## 6) Component Behavior and Accessibility Notes

- Minimum clickable target: 36x36 px for icon buttons, 40px height for standard controls.
- Contrast ratio: target WCAG AA for text and controls.
- Keyboard navigation: visible focus ring using Accent 500 at 2px.
- Form fields should show clear focus, error, and disabled states.
- Dialogs and drawers must trap focus and support ESC close.

---

## 7) Screen Inventory (Final)

1. Dashboard
2. Athletes
3. Fee Calculator
4. History
5. Settings

This structure is fixed and preserved exactly as required.

---

## 8) Handoff Notes

- This document defines visual system, layout architecture, interaction behavior, and localization rules for implementation.
- No backend/database logic is included in this specification.
- The UI should be implemented as a premium desktop admin experience optimized for operational speed and readability.
