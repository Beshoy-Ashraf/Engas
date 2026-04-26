document.addEventListener('DOMContentLoaded', () => {
    const loginForm = document.getElementById('loginForm');
    const togglePassword = document.getElementById('togglePassword');
    const passwordInput = document.getElementById('password');
    const loginBtn = loginForm.querySelector('.login-btn');

    const API_BASE_URL = 'http://192.168.1.15:5107';

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
                alert('يرجى ملء جميع الحقول');
                return;
            }
            requestBody = {
                StoreCode: storeId,
                Password: password
            };
            endpoint = `${API_BASE_URL}/api/auth/store/login`;
        } else if (isStaffLogin) {
            const username = document.getElementById('username').value.trim();
            if (!username || !password) {
                alert('يرجى ملء جميع الحقول');
                return;
            }
            requestBody = {
                UserName: username,
                Password: password
            };
            endpoint = `${API_BASE_URL}/api/auth/login`;
        } else {
            alert('تعذر تحديد نوع تسجيل الدخول');
            return;
        }

        // Set loading state
        const originalBtnContent = loginBtn.innerHTML;
        loginBtn.disabled = true;
        loginBtn.innerHTML = '<span>جاري تسجيل الدخول...</span> <i class="fas fa-spinner fa-spin"></i>';

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

                alert('تم تسجيل الدخول بنجاح!');

                // Redirect to main page (or dashboard)
                window.location.href = '../index.html';
            } else if (response.status === 401) {
                alert('اسم المستخدم أو كلمة المرور غير صحيحة');
            } else {
                const errorText = await response.text();
                console.error('Login error:', errorText);
                alert('حدث خطأ أثناء تسجيل الدخول، يرجى المحاولة مرة أخرى');
            }
        } catch (error) {
            console.error('Network error:', error);
            alert('تعذر الاتصال بالخادم، يرجى التحقق من اتصالك بالإنترنت');
        } finally {
            // Restore button state
            loginBtn.disabled = false;
            loginBtn.innerHTML = originalBtnContent;
        }
    });
});

