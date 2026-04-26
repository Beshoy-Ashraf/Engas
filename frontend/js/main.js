const menuToggle = document.getElementById('menuToggle');
const sidebar = document.getElementById('sidebar');

menuToggle.addEventListener('click', () => {
    sidebar.classList.toggle('active');
});

document.addEventListener('click', (e) => {
    if (window.innerWidth <= 1024) {
        if (!sidebar.contains(e.target) && !menuToggle.contains(e.target)) {
            sidebar.classList.remove('active ');
        }
    }
});

// Employee Modal Logic
document.addEventListener('DOMContentLoaded', () => {
    const addEmployeeBtn = document.getElementById('addEmployeeBtn');
    const employeeModal = document.getElementById('employeeModal');
    const employeeModalClose = document.getElementById('employeeModalClose');
    const employeeBtnCancel = document.getElementById('employeeBtnCancel');
    const employeeForm = document.getElementById('employeeForm');
    const employeePassword = document.getElementById('employeePassword');
    const employeeConfirmPassword = document.getElementById('employeeConfirmPassword');

    if (addEmployeeBtn && employeeModal) {
        addEmployeeBtn.addEventListener('click', () => {
            employeeModal.classList.add('active');
            document.body.style.overflow = 'hidden';
        });
    }

    function closeEmployeeModal() {
        if (employeeModal) {
            employeeModal.classList.remove('active');
            document.body.style.overflow = '';
        }
        // Reset form and indicators
        if (employeeForm) {
            employeeForm.reset();
        }
        const strengthBar = document.getElementById('employeeStrengthBar');
        if (strengthBar) strengthBar.style.width = '0%';
        const matchIndicator = document.getElementById('employeePasswordMatch');
        if (matchIndicator) {
            matchIndicator.textContent = '';
            matchIndicator.className = 'password-match';
        }
    }

    if (employeeModalClose) {
        employeeModalClose.addEventListener('click', closeEmployeeModal);
    }

    if (employeeBtnCancel) {
        employeeBtnCancel.addEventListener('click', closeEmployeeModal);
    }

    if (employeeModal) {
        employeeModal.addEventListener('click', (e) => {
            if (e.target === employeeModal) {
                closeEmployeeModal();
            }
        });
    }

    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape' && employeeModal && employeeModal.classList.contains('active')) {
            closeEmployeeModal();
        }
    });

    // Password visibility toggle
    window.togglePasswordVisibility = function(fieldId) {
        const field = document.getElementById(fieldId);
        const icon = field.parentElement.querySelector('.password-toggle i');
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

    // Password strength indicator
    if (employeePassword) {
        employeePassword.addEventListener('input', function() {
            const strengthBar = document.getElementById('employeeStrengthBar');
            const strength = checkPasswordStrength(this.value);
            strengthBar.style.width = strength.percentage + '%';
            strengthBar.style.backgroundColor = strength.color;
        });
    }

    // Password match validation
    if (employeeConfirmPassword) {
        employeeConfirmPassword.addEventListener('input', function() {
            const matchIndicator = document.getElementById('employeePasswordMatch');
            if (this.value === employeePassword.value) {
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
        employeeForm.addEventListener('submit', (e) => {
            e.preventDefault();

            if (employeePassword.value !== employeeConfirmPassword.value) {
                alert('كلمة السر وتأكيد كلمة السر غير متطابقين');
                return;
            }

            if (employeePassword.value.length < 8) {
                alert('كلمة المرور يجب أن تكون 8 أحرف على الأقل');
                return;
            }

            const formData = new FormData(employeeForm);
            const data = Object.fromEntries(formData.entries());
            console.log('Employee Data:', data);
            employeeForm.reset();
            closeEmployeeModal();
            showToast('تم إضافة الموظف بنجاح');
        });
    }

    // Toast functionality
    const successToast = document.getElementById('successToast');
    const toastClose = document.getElementById('toastClose');
    let toastTimeout;

    function showToast(message = 'تم الحفظ بنجاح') {
        if (successToast) {
            const toastMessage = successToast.querySelector('#toastMessage') || successToast.querySelector('p');
            if (toastMessage) toastMessage.textContent = message;
            successToast.classList.add('show');
            clearTimeout(toastTimeout);
            toastTimeout = setTimeout(() => {
                hideToast();
            }, 3000);
        }
    }

    function hideToast() {
        if (successToast) {
            successToast.classList.remove('show');
        }
    }

    if (toastClose) {
        toastClose.addEventListener('click', hideToast);
    }

    // Password strength checker
    function checkPasswordStrength(password) {
        let strength = 0;
        const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
        
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
});

document.addEventListener('click', (e) => {
    if (window.innerWidth <= 1024) {
        if (!sidebar.contains(e.target) && !menuToggle.contains(e.target)) {
            sidebar.classList.remove('active');
        }
    }
});

/* ============================================
   CUSTOMER MODAL LOGIC
   ============================================ */
document.addEventListener('DOMContentLoaded', () => {
    // Toast functionality only - no models on index page
});

    }

    function closeBranchModal() {
        if (branchModal) {
            branchModal.classList.remove('active');
            document.body.style.overflow = '';
        }
    }

    if (branchModalClose) {
        branchModalClose.addEventListener('click', closeBranchModal);
    }

    if (branchBtnCancel) {
        branchBtnCancel.addEventListener('click', closeBranchModal);
    }

    if (branchModal) {
        branchModal.addEventListener('click', (e) => {
            if (e.target === branchModal) {
                closeBranchModal();
            }
        });
    }

    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape' && branchModal && branchModal.classList.contains('active')) {
            closeBranchModal();
        }
    });

    if (branchForm) {
        branchForm.addEventListener('submit', (e) => {
            e.preventDefault();
            const password = document.getElementById('branchPassword').value;
            const confirmPassword = document.getElementById('branchConfirmPassword').value;

            if (password !== confirmPassword) {
                alert('كلمة السر وتأكيد كلمة السر غير متطابقين');
                return;
            }

            const formData = new FormData(branchForm);
            const data = Object.fromEntries(formData.entries());
            console.log('Branch Data:', data);
            branchForm.reset();
            closeBranchModal();
            showToast();
        });
    }
});
