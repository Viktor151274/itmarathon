# How to Fork a GitHub Project Without Using Fork

## Step 1: Download the Project as a ZIP Archive

First, you need to download all the project files from the original repository to your computer. **Check your email** for the invitation message from the IT-Marathon team. It will contain a direct download link to the ZIP archive.

### Option A: Linux Console (Terminal)

If you prefer working in the terminal, this is the fastest method:

1. **Open Terminal** (Ctrl+Alt+T)

2. **Navigate to your desired folder** (e.g., Downloads):
   ```bash
   cd ~/Downloads
   ```

3. **Download the ZIP archive** using wget:
   ```bash
   wget -O marathon2025.zip "YOUR_DOWNLOAD_LINK_FROM_EMAIL"
   ```
   *Replace `YOUR_DOWNLOAD_LINK_FROM_EMAIL` with the actual link from your email.*

4. **Install unzip** if not already installed:
   ```bash
   sudo apt-get install unzip
   ```

5. **Extract the archive:**
   ```bash
   unzip marathon2025.zip
   ```

6. **Navigate into the extracted folder:**
   ```bash
   cd marathon2025-main
   ```
   *Note: The folder name might be different. Use `ls` to see available folders.*

**Done!** You can now proceed to Step 2.

---

### Option B: Windows (File Explorer)

1. **Click the download link** in your email. The browser will download the ZIP file to your "Downloads" folder.

2. **Open File Explorer** (Win + E) and go to "Downloads".

3. **Right-click on the ZIP file** → Select **Extract All...** → Click **Extract**.

---

### Option C: Linux (File Manager)

1. **Click the download link** in your email. The browser will download the ZIP file to your "Downloads" folder.

2. **Open File Manager** and go to "Downloads".

3. **Right-click on the ZIP file** → Select **Extract Here**.

---

## Step 2: Create a New Repository on Your Own GitHub Account

Now you need to create an empty repository on your account where you will upload the project files.

1. **Log in to your GitHub account** at [github.com](https://github.com)

2. **Click the "+" icon** in the top-right corner, next to your profile picture.

3. **Choose "New repository"** from the dropdown menu.

   ![Create new repository](assets/zip-manual/2.jpg)

4. **Fill in the new repository information:**
   - **Repository name:** Enter a name for your project (e.g., `my-marathon-project`)
   - **Description (optional):** Add a short description of the project
   - **Public/Private:** Choose **Private** to make your repository not visible to everyone
   
   **IMPORTANT:** Do not check the boxes for "Add a README file", "Add .gitignore", or "Choose a license". The repository must be completely empty.

5. **Click the "Create repository" button.**

   ![Create repository button](assets/zip-manual/3.jpg)

---

## Step 3: Upload the Project Files to Your New Repository

### 3.1. Install Git (if not already installed)

   **Windows:**

   - Download Git from https://git-scm.com/download/win
   - Run the installer and click "Next" through all steps (default settings are fine)
   - After installation, open **Git Bash** (search for "Git Bash" in Start menu)

   **Linux:**

   - Open **Terminal**
   - Run the command:
   ```bash
   sudo apt-get install git
   ```
   - Enter your password when prompted

### 3.2. Navigate to Your Project Folder

1. **Find the full path to your extracted folder:**
   - **Windows:** Open the folder in File Explorer, click on the address bar at the top, and copy the path (e.g., `C:\Users\YourName\Downloads\marathon2025-main`)
   - **Linux:** Open the folder in File Manager, right-click and select "Properties" to see the path (e.g., `/home/yourname/Downloads/marathon2025-main`)

2. **Open the terminal:**
   - **Windows:** Open **Git Bash** (search in Start menu)
   - **Linux:** Open **Terminal** (Ctrl+Alt+T)

3. **Navigate to the folder** by typing `cd` followed by the path:
   
   **Windows example:**
   ```bash
   cd /c/Users/YourName/Downloads/marathon2025-main
   ```
   
   **Linux example:**
   ```bash
   cd /home/yourname/Downloads/marathon2025-main
   ```
   
   Press **Enter**.

### 3.3. Configure Git (First Time Only)

If this is your first time using Git, you need to tell it who you are:

```bash
git config --global user.name "Your Name"
git config --global user.email "your.email@example.com"
```

Replace with your actual name and the email you use for GitHub.

### 3.4. Upload Files to GitHub

1. **Initialize Git in the folder:**
   ```bash
   git init
   ```
   ![Git init result](assets/zip-manual/4.jpg)

2. **Connect to your GitHub repository:**
   ```bash
   git remote add origin https://github.com/YOUR-USERNAME/my-marathon-project.git
   ```
   
   **Important:** Replace `YOUR-USERNAME` with your actual GitHub username and `my-marathon-project` with your repository name!
   
   **Tip:** You can copy this URL from your empty repository page on GitHub.

   ![Empty repository page with upload link highlighted](assets/zip-manual/5.jpg)

3. **Add all files (including hidden ones):**
   ```bash
   git add .
   ```

4. **Save the changes with a message:**
   ```bash
   git commit -m "Initial project setup"
   ```

5. **Set the main branch name:**
   ```bash
   git branch -M main
   ```

6. **Upload everything to GitHub:**
   ```bash
   git push -u origin main
   ```
   
   You may be asked to log in to GitHub. Follow the instructions in the terminal.

### 3.5. Verify the Upload

Go to your repository page on GitHub and refresh it. You should now see all the project files, including hidden ones! Next manuals you will find on the front page of your repository.

![Uploaded repository](assets/zip-manual/6.jpg)

---

## Done!