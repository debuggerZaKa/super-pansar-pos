
# 🛒 Super Pansar - POS & Inventory Management System

A **Comprehensive Point of Sale, Inventory and Resource Tracking System** built with **C# Windows Forms**.  
This application is designed for businesses (shops, marts, warehouses) to manage **sales, stock, accounts and workers** in a seamless way.  

---

## ✨ Features

### 🔐 User & Account Management
- **Admin Accounts**: Manage overall system operations.  
- **Worker Accounts**: Limited access for workers.  
- **Login System**: Secure login for admins and workers.  

### 📦 Inventory & Stock
- **Add Item**: Add new products to stock.  
- **Barcode**: Generate and print barcode for products. 
- **Stock Tracking**: View and update available stock.  
- **Warehouses**: Manage multiple warehouses.  
- **Warehouse Transfer**: Transfer stock between warehouses.  

### 🧾 Billing & Invoices
- **Create Invoice**: Generate customer bills.  
- **Invoice List**: Track past invoices.  
- **Hidden Invoices**: Manage sensitive invoices.  
- **Crystal Reports**: Generate detailed reports (`CrystalReport1.rpt`, `CrystalReport2.rpt`).  

### 📊 Financial Management
- **Ledger**: Maintain detailed ledger records.  
- **Market Ledger**: Track external market transactions.  
- **Expense Tracking**: Record and monitor expenses.  
- **Manual Entry**: Add custom transactions manually.  

### 👥 Workforce Management
- **Workers**: Track worker details and activities.  
- **Workers Entry**: Maintain worker-related records. 
- **Workers Salary**: Keep track of workerssalary. 

### ⚙️ Additional
- **Settings**: Customize system preferences.  
- **Secure Exit & Logout**.  

---

## 📂 Project Structure

```
WindowsFormsApplication1/
│
├── Properties/                  # Application metadata & settings
│   ├── AssemblyInfo.cs
│   ├── Resources.resx
│   └── Settings.settings
│
├── References/                  # .NET dependencies & NuGet packages
│
├── Reports & Data
│   ├── CrystalReport1.rpt
│   ├── CrystalReport2.rpt
│   └── DataSethvoice.xsd
│
├── Core Forms
│   ├── Form1.cs                 # Main form
│   ├── Super_pansar menu.cs     # Dashboard / Main menu
│   ├── stock.cs                 # Stock management
│   ├── expense.cs               # Expense management
│   ├── Ledger.cs                # Ledger form
│   ├── MarketLedger.cs          # Market ledger
│   ├── manual_entry.cs          # Manual entries
│   ├── HiddenInvoices.cs        # Hidden invoices
│   ├── Warehouses.cs            # Warehouse management
│   ├── WarehouseTransfer.cs     # Transfer between warehouses
│   ├── Workers.cs               # Worker management
│   ├── WorkersEntry.cs          # Worker entries
│   ├── workers_login.cs         # Worker login
│   ├── create_adminAcc.cs       # Create admin accounts
│   ├── create_workerAcc.cs      # Create worker accounts
│   ├── settings.cs              # Settings form
│   ├── PayBill.cs               # Bill payment
│   ├── Super Pansar store invoice.cs  # Invoice handling
│   ├── SuperPansar store Billing.cs   # Billing system
│   └── ManualProducts.cs        # Manual product management
│
├── Assets
│   └── calculator.ico           # Application icon
│
├── Program.cs                   # Entry point
├── packages.config              # NuGet package configuration
└── WindowsFormsApplication1_3_TemporaryKey.pfx # Signing key
```

---

## 🛠️ Technologies Used
- **Language**: C#  
- **Framework**: .NET (Windows Forms)  
- **Database**:  SQL Server for multiple users / Local DB for standalone  
- **Reporting**: Crystal Reports  
- **UI**: Windows Forms custom UI  

---

## 🚀 Getting Started

1. Clone the repository:  
   ```bash
   git clone https://github.com/debuggerZaka/super-pansar-pos.git
   ```
2. Open `WindowsFormsApplication1.sln` in **Visual Studio**.  
3. Restore NuGet packages (`packages.config`).  
4. Build & Run the project (`Program.cs` is the entry point).  
5. Configure database connection if required.  

---

## 📬 Contact

For queries, suggestions, or collaborations, feel free to reach out:

- **Email**: tahirzaka10@gmail.com  
- **Phone**: +92 319 7656098  
- **Portfolio**: (https://tahirzaka-portfolio.vercel.app/)
