const menuToggle = document.getElementById('menuToggle');
const sidebar = document.getElementById('sidebar');

menuToggle.addEventListener('click', () => {
    sidebar.classList.toggle('active');
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
    /* ============================================
       CUSTOMER MODAL LOGIC
       ============================================ */
    const addCustomerBtn = document.getElementById('addCustomerBtn');
    const customerModal = document.getElementById('customerModal');
    const modalClose = document.getElementById('modalClose');
    const btnCancel = document.getElementById('btnCancel');
    const customerForm = document.getElementById('customerForm');

    if (addCustomerBtn && customerModal) {
        addCustomerBtn.addEventListener('click', () => {
            customerModal.classList.add('active');
            document.body.style.overflow = 'hidden';
        });
    }

    function closeCustomerModal() {
        if (customerModal) {
            customerModal.classList.remove('active');
            document.body.style.overflow = '';
        }
    }

    if (modalClose) {
        modalClose.addEventListener('click', closeCustomerModal);
    }

    if (btnCancel) {
        btnCancel.addEventListener('click', closeCustomerModal);
    }

    if (customerModal) {
        customerModal.addEventListener('click', (e) => {
            if (e.target === customerModal) {
                closeCustomerModal();
            }
        });
    }

    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape' && customerModal && customerModal.classList.contains('active')) {
            closeCustomerModal();
        }
    });

    const successToast = document.getElementById('successToast');
    const toastClose = document.getElementById('toastClose');
    let toastTimeout;

    function showToast() {
        if (successToast) {
            successToast.classList.add('show');
            clearTimeout(toastTimeout);
            toastTimeout = setTimeout(() => {
                hideToast();
            }, 4000);
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

    if (customerForm) {
        customerForm.addEventListener('submit', (e) => {
            e.preventDefault();
            const formData = new FormData(customerForm);
            const data = Object.fromEntries(formData.entries());
            console.log('Customer Data:', data);
            customerForm.reset();
            closeCustomerModal();
            showToast();
        });
    }

    /* ============================================
       BRANCH MODAL LOGIC
       ============================================ */
    const addBranchBtn = document.getElementById('addBranchBtn');
    const branchModal = document.getElementById('branchModal');
    const branchModalClose = document.getElementById('branchModalClose');
    const branchBtnCancel = document.getElementById('branchBtnCancel');
    const branchForm = document.getElementById('branchForm');

    if (addBranchBtn && branchModal) {
        addBranchBtn.addEventListener('click', () => {
            branchModal.classList.add('active');
            document.body.style.overflow = 'hidden';
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
