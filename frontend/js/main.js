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

    function closeModal() {
        if (customerModal) {
            customerModal.classList.remove('active');
            document.body.style.overflow = '';
        }
    }

    if (modalClose) {
        modalClose.addEventListener('click', closeModal);
    }

    if (btnCancel) {
        btnCancel.addEventListener('click', closeModal);
    }

    if (customerModal) {
        customerModal.addEventListener('click', (e) => {
            if (e.target === customerModal) {
                closeModal();
            }
        });
    }

    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape' && customerModal && customerModal.classList.contains('active')) {
            closeModal();
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
            closeModal();
            showToast();
        });
    }
});
