// Employee Modal functionality for index.html
console.log('employee-modal.js loaded');

(function() {
    // Wait for DOM to be ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }

    function init() {
        const addEmployeeBtn = document.getElementById('addEmployeeBtn');
        const employeeModal = document.getElementById('employeeModal');
        const employeeModalClose = document.getElementById('employeeModalClose');
        const employeeBtnCancel = document.getElementById('employeeBtnCancel');
        const employeeForm = document.getElementById('employeeForm');

        if (!addEmployeeBtn || !employeeModal) {
            console.error('Employee modal elements not found');
            return;
        }

        // Open modal
        addEmployeeBtn.addEventListener('click', function(e) {
            e.preventDefault();
            employeeModal.classList.add('active');
            document.body.style.overflow = 'hidden';
        });

        // Close modal function
        function closeModal() {
            employeeModal.classList.remove('active');
            document.body.style.overflow = '';
            if (employeeForm) {
                employeeForm.reset();
            }
            // Reset password indicators
            const strengthBar = document.getElementById('employeeStrengthBar');
            if (strengthBar) strengthBar.style.width = '0%';
            const matchIndicator = document.getElementById('employeePasswordMatch');
            if (matchIndicator) {
                matchIndicator.textContent = '';
                matchIndicator.className = 'password-match';
            }
        }

        // Close buttons
        if (employeeModalClose) {
            employeeModalClose.addEventListener('click', closeModal);
        }
        if (employeeBtnCancel) {
            employeeBtnCancel.addEventListener('click', closeModal);
        }

        // Close on outside click
        employeeModal.addEventListener('click', function(e) {
            if (e.target === employeeModal) {
                closeModal();
            }
        });

        // Close on Escape key
        document.addEventListener('keydown', function(e) {
            if (e.key === 'Escape' && employeeModal.classList.contains('active')) {
                closeModal();
            }
        });

        // Password visibility toggle
        window.togglePasswordVisibility = function(fieldId) {
            const field = document.getElementById(fieldId);
            if (!field) return;
            const toggleBtn = field.parentElement.querySelector('.password-toggle');
            if (!toggleBtn) return;
            const icon = toggleBtn.querySelector('i');
            
            if (field.type === 'password') {
                field.type = 'text';
                icon.classList.remove('fa-eye');
                icon.classList.add('fa-eye-slash');
            } else {
                field.type = 'password';
                icon.classList.remove('fa-eye-slash');
                icon.classList.add('fa-eye');
            }
        };

        // Password strength checker
        function checkPasswordStrength(password) {
            let strength = 0;
            
            if (password.length >= 8) strength += 25;
            if (password.match(/[a-z]/)) strength += 25;
            if (password.match(/[A-Z]/)) strength += 25;
            if (password.match(/[0-9]/)) strength += 15;
            if (password.match(/[^a-zA-Z0-9]/)) strength += 10;

            if (strength < 30) return { percentage: strength, color: '#ff4757' };
            if (strength < 60) return { percentage: strength, color: '#ffa502' };
            if (strength < 90) return { percentage: strength, color: '#ffdd59' };
            return { percentage: 100, color: '#5DD62C' };
        }

        // Password strength indicator
        const passwordField = document.getElementById('employeePassword');
        const strengthBar = document.getElementById('employeeStrengthBar');
        
        if (passwordField && strengthBar) {
            passwordField.addEventListener('input', function() {
                const strength = checkPasswordStrength(this.value);
                strengthBar.style.width = strength.percentage + '%';
                strengthBar.style.backgroundColor = strength.color;
            });
        }

        // Password match validation
        const confirmPasswordField = document.getElementById('employeeConfirmPassword');
        const matchIndicator = document.getElementById('employeePasswordMatch');
        
        if (confirmPasswordField && passwordField && matchIndicator) {
            confirmPasswordField.addEventListener('input', function() {
                if (this.value === passwordField.value) {
                    matchIndicator.textContent = '✓ متطابقة';
                    matchIndicator.className = 'password-match match';
                } else {
                    matchIndicator.textContent = '✗ غير متطابقة';
                    matchIndicator.className = 'password-match mismatch';
                }
            });
        }

        // Form submission
        if (employeeForm) {
            employeeForm.addEventListener('submit', function(e) {
                e.preventDefault();

                const password = document.getElementById('employeePassword').value;
                const confirmPassword = document.getElementById('employeeConfirmPassword').value;

                if (password !== confirmPassword) {
                    alert('كلمة السر وتأكيد كلمة السر غير متطابقين');
                    return;
                }

                if (password.length < 8) {
                    alert('كلمة المرور يجب أن تكون 8 أحرف على الأقل');
                    return;
                }

                // Form is valid - submit
                const formData = new FormData(employeeForm);
                const data = Object.fromEntries(formData.entries());
                console.log('Employee Data:', data);
                
                closeModal();
                showToast('تم إضافة الموظف بنجاح');
            });
        }
    }

    // Toast notification (global)
    window.showToast = function(message) {
        const toast = document.getElementById('successToast');
        const toastMessage = document.getElementById('toastMessage');
        if (toast && toastMessage) {
            toastMessage.textContent = message;
            toast.classList.add('show');
            setTimeout(function() {
                toast.classList.remove('show');
            }, 3000);
        }
    };

    window.hideToast = function() {
        const toast = document.getElementById('successToast');
        if (toast) {
            toast.classList.remove('show');
        }
    };

    // Close toast on click
    const toastCloseBtn = document.getElementById('toastClose');
    if (toastCloseBtn) {
        toastCloseBtn.addEventListener('click', hideToast);
    }
})();
