document.addEventListener('DOMContentLoaded', function () {
    // Elements
    const branchForm = document.getElementById('branchForm');
    const steps = document.querySelectorAll('.form-step');
    const stepIndicators = document.querySelectorAll('.step');
    const prevBtn = document.getElementById('prevBtn');
    const nextBtn = document.getElementById('nextBtn');
    const submitBtn = document.getElementById('submitBtn');
    const successToast = document.getElementById('successToast');
    const toastClose = document.getElementById('toastClose');

    let currentStep = 1;
    const totalSteps = steps.length;

    // Password visibility toggle
    window.togglePassword = function (inputId) {
        const input = document.getElementById(inputId);
        const icon = input.parentElement.querySelector('.password-toggle i');

        if (input.type === 'password') {
            input.type = 'text';
            icon.classList.remove('fa-eye');
            icon.classList.add('fa-eye-slash');
        } else {
            input.type = 'password';
            icon.classList.remove('fa-eye-slash');
            icon.classList.add('fa-eye');
        }
    };

    // Update step indicators
    function updateSteps() {
        // Update form steps visibility
        steps.forEach(step => {
            step.classList.remove('active');
        });
        document.querySelector(`.form-step[data-step="${currentStep}"]`).classList.add('active');

        // Update progress indicators
        stepIndicators.forEach(indicator => {
            const stepNum = parseInt(indicator.dataset.step);
            indicator.classList.remove('active', 'completed');

            if (stepNum === currentStep) {
                indicator.classList.add('active');
            } else if (stepNum < currentStep) {
                indicator.classList.add('completed');
            }
        });

        // Update buttons
        if (currentStep === 1) {
            prevBtn.style.display = 'none';
        } else {
            prevBtn.style.display = 'flex';
        }

        if (currentStep === totalSteps) {
            nextBtn.style.display = 'none';
            submitBtn.style.display = 'flex';
            updateSummary();
        } else {
            nextBtn.style.display = 'flex';
            submitBtn.style.display = 'none';
        }

        // Scroll to top of form
        document.querySelector('.register-card').scrollIntoView({ behavior: 'smooth', block: 'start' });
    }

    // Validate current step
    function validateStep(step) {
        const currentStepEl = document.querySelector(`.form-step[data-step="${step}"]`);
        const inputs = currentStepEl.querySelectorAll('[required]');
        let isValid = true;

        inputs.forEach(input => {
            if (!input.value.trim()) {
                showError(input, 'هذا  الحقل مطلوب');
                isValid = false;
            } else {
                clearError(input);
                // Specific validations
                if (input.id === 'branchCode') {
                    if (input.value.length < 3) {
                        showError(input, 'كود الفرع قصير جدًا');
                        isValid = false;
                    }
                }
                if (input.id === 'branchPhone') {
                    const phoneRegex = /^0\d{10}$/;
                    if (!phoneRegex.test(input.value.replace(/\s/g, ''))) {
                        showError(input, 'رقم الهاتف غير صحيح');
                        isValid = false;
                    }
                }
            }
        });

        // Step-specific validations
        if (step === 4) {
            const password = document.getElementById('branchPassword').value;
            const confirmPassword = document.getElementById('branchConfirmPassword').value;

            if (password.length < 6) {
                showError(document.getElementById('branchPassword'), 'كلمة المرور يجب أن تكون 6 أحرف على الأقل');
                isValid = false;
            }

            if (password !== confirmPassword) {
                showError(document.getElementById('branchConfirmPassword'), 'كلمتي المرور غير متطابقتين');
                isValid = false;
            }
        }

        return isValid;
    }

    function showError(input, message) {
        input.classList.add('error');
        let errorEl = input.parentElement.querySelector('.error-message');
        if (!errorEl) {
            errorEl = document.createElement('div');
            errorEl.className = 'error-message';
            input.parentElement.appendChild(errorEl);
        }
        errorEl.textContent = message;
    }

    function clearError(input) {
        input.classList.remove('error');
        const errorEl = input.parentElement.querySelector('.error-message');
        if (errorEl) {
            errorEl.remove();
        }
    }

    // Update summary
    function updateSummary() {
        document.getElementById('summaryName').textContent = document.getElementById('branchName').value || '-';
        document.getElementById('summaryCode').textContent = document.getElementById('branchCode').value || '-';
        document.getElementById('summaryGov').textContent = document.getElementById('branchGovernorate').value || '-';
        document.getElementById('summaryArea').textContent = document.getElementById('branchArea').value || '-';
        document.getElementById('summaryManager').textContent = document.getElementById('branchManager').value || '-';
        document.getElementById('summaryPhone').textContent = document.getElementById('branchPhone').value || '-';
    }

    // Password strength indicator
    const passwordInput = document.getElementById('branchPassword');
    if (passwordInput) {
        passwordInput.addEventListener('input', function () {
            const strengthBar = document.querySelector('.strength-bar');
            const password = this.value;
            let strength = 0;

            if (password.length >= 6) strength += 25;
            if (password.length >= 10) strength += 25;
            if (/[A-Za-z]/.test(password) && /[0-9]/.test(password)) strength += 25;
            if (/[!@#$%^&*]/.test(password)) strength += 25;

            strengthBar.style.width = strength + '%';

            if (strength <= 25) {
                strengthBar.style.background = '#ff4757';
            } else if (strength <= 50) {
                strengthBar.style.background = '#ffa502';
            } else if (strength <= 75) {
                strengthBar.style.background = '#ffdd59';
            } else {
                strengthBar.style.background = '#5DD62C';
            }
        });
    }

    // Password match check
    const confirmPasswordInput = document.getElementById('branchConfirmPassword');
    const passwordMatchDiv = document.getElementById('passwordMatch');

    if (confirmPasswordInput) {
        confirmPasswordInput.addEventListener('input', function () {
            const password = document.getElementById('branchPassword').value;
            const confirm = this.value;

            if (confirm && password !== confirm) {
                showError(this, 'كلمتا المرور غير متطابقتين');
                passwordMatchDiv.textContent = '❌ غير متطابق';
                passwordMatchDiv.classList.add('error');
            } else if (confirm && password === confirm) {
                clearError(this);
                passwordMatchDiv.textContent = '✅ متطابق';
                passwordMatchDiv.classList.remove('error');
                passwordMatchDiv.classList.add('success');
            } else {
                passwordMatchDiv.textContent = '';
                passwordMatchDiv.classList.remove('error', 'success');
            }
        });
    }

    // Event Listeners
    prevBtn.addEventListener('click', () => {
        if (currentStep > 1) {
            currentStep--;
            updateSteps();
        }
    });

    nextBtn.addEventListener('click', () => {
        if (validateStep(currentStep)) {
            currentStep++;
            updateSteps();
        }
    });

    // Form submission
    branchForm.addEventListener('submit', function (e) {
        e.preventDefault();

        if (!validateStep(currentStep)) {
            return;
        }

        const formData = new FormData(this);
        const data = Object.fromEntries(formData.entries());

        console.log('Branch Data:', data);

        // Simulate API call
        setTimeout(() => {
            // Show success message
            successToast.classList.add('show');

            // Reset form
            branchForm.reset();
            currentStep = 1;
            updateSteps();

            // Redirect
            setTimeout(() => {
                window.location.href = '../index.html';
            }, 2000);
        }, 500);
    });

    // Real-time validation
    document.querySelectorAll('.form-input, .form-select').forEach(input => {
        input.addEventListener('blur', function () {
            if (this.hasAttribute('required') && !this.value.trim()) {
                showError(this, 'هذا الحقل مطلوب');
            } else {
                clearError(this);
            }
        });

        input.addEventListener('input', function () {
            if (this.classList.contains('error')) {
                clearError(this);
            }
        });
    });

    // Toast close
    if (toastClose) {
        toastClose.addEventListener('click', () => {
            successToast.classList.remove('show');
        });
    }

    // Auto-hide toast
    setTimeout(() => {
        successToast.classList.remove('show');
    }, 5000);

    // Initialize
    updateSteps();
});
