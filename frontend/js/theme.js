const ThemeManager = {
    init() {
        this.body = document.body;
        this.themeToggle = document.getElementById('themeToggle');
        this.loadTheme();
        this.bindEvents();
    },

    loadTheme() {
        const savedTheme = localStorage.getItem('engas-theme') || 'dark';
        this.body.setAttribute('data-theme', savedTheme);
        this.updateUI(savedTheme);
    },

    toggleTheme() {
        const currentTheme = this.body.getAttribute('data-theme');
        const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
        this.body.setAttribute('data-theme', newTheme);
        localStorage.setItem('engas-theme', newTheme);
        this.updateUI(newTheme);
    },

    updateUI(theme) {
        if (!this.themeToggle) return;

        if (this.themeToggle.classList.contains('theme-toggle-modern')) {
            this.updateModernToggle(theme);
        } else {
            this.updateIconToggle(theme);
        }
    },

    updateModernToggle(theme) {
        const slider = this.themeToggle.querySelector('.toggle-slider');
        
        if (theme === 'dark') {
            this.themeToggle.classList.remove('light');
            this.themeToggle.title = 'تبديل للوضع الفاتح';
        } else {
            this.themeToggle.classList.add('light');
            this.themeToggle.title = 'تبديل للوضع الداكن';
        }
    },

    updateIconToggle(theme) {
        const icon = this.themeToggle.querySelector('i');
        if (!icon) return;

        icon.classList.add('rotate-out');

        setTimeout(() => {
            if (theme === 'dark') {
                icon.className = 'fas fa-sun rotate-in';
                this.themeToggle.title = 'تبديل للوضع الفاتح';
            } else {
                icon.className = 'fas fa-moon rotate-in';
                this.themeToggle.title = 'تبديل للوضع الداكن';
            }

            setTimeout(() => {
                icon.classList.remove('rotate-in', 'rotate-out');
            }, 500);
        }, 300);
    },

    bindEvents() {
        if (this.themeToggle) {
            this.themeToggle.addEventListener('click', () => this.toggleTheme());
        }
    }
};

document.addEventListener('DOMContentLoaded', () => ThemeManager.init());