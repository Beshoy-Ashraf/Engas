document.addEventListener('DOMContentLoaded', () => {
    const loginForm = document.getElementById('loginForm');
    const togglePassword = document.getElementById('togglePassword');
    const passwordInput = document.getElementById('password');
    const loginBtn = loginForm.querySelector('.login-btn');

    const API_BASE_URL = 'https://engas-production.up.railway.app'; // Update with your actual API base URL

    // Toast message (shows inline on the page with animation)
    let toastTimer = null;
    function showToast(type, message) {
        try {
            const existing = document.querySelector('.toast-message');
            if (existing) existing.remove();
            if (toastTimer) clearTimeout(toastTimer);

            const toast = document.createElement('div');
            toast.className = `toast-message ${type}`;

            const icon = type === 'success'
                ? '<i class="fas fa-check-circle"></i>'
                : type === 'error'
                    ? '<i class="fas fa-times-circle"></i>'
                    : '<i class="fas fa-info-circle"></i>';

            toast.innerHTML = `
                <div class="toast-row">
                    ${icon}
                    <span>${message}</span>
                </div>
            `;

            document.body.appendChild(toast);

            requestAnimationFrame(() => toast.classList.add('show'));

            toastTimer = setTimeout(() => {
                toast.classList.remove('show');
                toast.classList.add('hide');
                setTimeout(() => toast.remove(), 260);
            }, 3200);
        } catch (e) {
            console.error('showToast error:', e);
            alert(message);
        }
    }

    // Detect login type based on which input field exists
    const isStoreLogin = !!document.getElementById('storeId');
    const isStaffLogin = !!document.getElementById('username');

    // Toggle password visibility
    togglePassword.addEventListener('click', () => {
        const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
        passwordInput.setAttribute('type', type);
        togglePassword.classList.toggle('fa-eye ');
        togglePassword.classList.toggle('fa-eye-slash');
    });

    // Handle form submission
    loginForm.addEventListener('submit', async (e) => {
        e.preventDefault();

        const password = document.getElementById('password').value;
        const remember = document.getElementById('remember').checked;

        let requestBody = {};
        let endpoint = '';

        if (isStoreLogin) {
            const storeId = document.getElementById('storeId').value.trim();
            if (!storeId || !password) {
                showToast('error', 'يرجى ملء جميع الحقول');
                return;
            }
            requestBody = {
                StoreCode: storeId,
                Password: password
            };
            endpoint = `${API_BASE_URL}/api/authentication/store-login`;
        } else if (isStaffLogin) {
            const username = document.getElementById('username').value.trim();
            if (!username || !password) {
                showToast('error', 'يرجى ملء جميع الحقول');
                return;
            }
            requestBody = {
                UserName: username,
                Password: password
            };
            endpoint = `${API_BASE_URL}/api/authentication/staff-login`;
        } else {
            showToast('error', 'تعذر تحديد نوع تسجيل الدخول');
            return;
        }

        // Set loading state
        const originalBtnContent = loginBtn.innerHTML;
        loginBtn.disabled = true;
        loginBtn.innerHTML = '<span>جارى تسجيل الدخول...</span> <i class="fas fa-spinner fa-spin"></i>';

        try {
            const response = await fetch(endpoint, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(requestBody)
            });

            if (response.ok) {
                const data = await response.json();

                // Store tokens
                const storage = remember ? localStorage : sessionStorage;
                storage.setItem('token', data.token);
                storage.setItem('refreshToken', data.refreshToken);
                storage.setItem('userId', data.userId);
                storage.setItem('loginType', isStoreLogin ? 'store' : 'staff');
                storage.setItem('remember', remember.toString());

                showToast('success', 'تم تسجيل الدخول بنجاح!');

                // Redirect to main page (or dashboard)
                window.location.href = '../index.html';
            } else if (response.status === 401) {
                showToast('error', 'اسم المستخدم أو كلمة المرور غير صحيحة');
            } else {
                const errorText = await response.text();
                console.error('Login error:', errorText);
                showToast('error', 'حدث خطأ أثناء تسجيل الدخول، يرجى المحاولة مرة أخرى');
            }
        } catch (error) {
            console.error('Network error:', error);
            showToast('error', 'تعذر الاتصال بالخادم، يرجى التحقق من اتصالك بالإنترنت');
        } finally {
            // Restore button state
            loginBtn.disabled = false;
            loginBtn.innerHTML = originalBtnContent;
        }
    });
});

