// Use global employees data from employees-data.js
let employees = window.employees || [];

// Role icons mapping
const roleIcons = {
    'manager': 'fa-user-tie',
    'supervisor': 'fa-user-shield',
    'sales': 'fa-user-plus',
    'support': 'fa-headset ',
    'warehouse': 'fa-boxes'
};

// Initialize the page
document.addEventListener('DOMContentLoaded', () => {
    loadEmployees();
    updateStats();
    initializeModals();
});

// Load employees into grid
function loadEmployees(data = employees) {
    const grid = document.getElementById('employeesGrid');
    grid.innerHTML = '';

    if (data.length === 0) {
        document.getElementById('noResults').style.display = 'flex';
        return;
    }

    document.getElementById('noResults').style.display = 'none';

    data.forEach((employee, index) => {
        const card = createEmployeeCard(employee);
        card.style.animationDelay = `${index * 0.05}s`;
        grid.appendChild(card);
    });
}

// Create employee card element
function createEmployeeCard(employee) {
    const card = document.createElement('div');
    card.className = 'employee-card';
    card.onclick = () => openEmployeeDetailModal(employee.id);

    const statusClass = employee.status === 'active' ? 'status-active' : 'status-inactive';
    const statusIcon = employee.status === 'active' ? 'fa-check-circle' : 'fa-times-circle';
    const statusText = employee.status === 'active' ? 'نشط' : 'غير نشط';
    const roleIcon = roleIcons[employee.role] || 'fa-user';

    const initials = employee.name.split(' ').map(n => n[0]).join('').substring(0, 2);

    card.innerHTML = `
        <div class="employee-card-header">
            <div class="employee-avatar">
                <i class="fas ${roleIcon}"></i>
            </div>
            <div class="employee-info">
                <div class="employee-name">${employee.name}</div>
                <div class="employee-code">${employee.code}</div>
                <div class="employee-role">${employee.roleLabel}</div>
            </div>
            <div class="employee-status ${statusClass}">
                <i class="fas ${statusIcon}"></i>
                ${statusText}
            </div>
        </div>
        <div class="employee-details">
            <div class="detail-row">
                <i class="fas fa-id-badge"></i>
                <span>${employee.number}</span>
            </div>
            <div class="detail-row">
                <i class="fas fa-phone"></i>
                <span>${employee.phone}</span>
            </div>
            <div class="detail-row">
                <i class="fas fa-envelope"></i>
                <span>${employee.email}</span>
            </div>
        </div>
        <div class="employee-actions">
            <button class="action-btn-icon edit-btn" onclick="event.stopPropagation(); openEditModal(${employee.id})" title="تعديل">
                <i class="fas fa-edit"></i>
            </button>
            <button class="action-btn-icon delete-btn" onclick="event.stopPropagation(); openDeleteModal(${employee.id})" title="حذف">
                <i class="fas fa-trash"></i>
            </button>
        </div>
    `;

    return card;
}

// Open employee detail modal
function openEmployeeDetailModal(employeeId) {
    const employee = employees.find(e => e.id === employeeId);
    if (!employee) return;

    const modalBody = document.getElementById('employeeDetailBody');
    const roleIcon = roleIcons[employee.role] || 'fa-user';

    const statusClass = employee.status === 'active' ? 'status-active' : 'status-inactive';
    const statusText = employee.status === 'active' ? 'نشط' : 'غير نشط';

    // Generate permissions list
    const permissionsMap = {
        'all': 'جميع الصلاحيات',
        'branches': 'إدارة الفروع',
        'customers': 'إدارة العملاء',
        'sales': 'المبيعات',
        'support': 'الدعم الفني',
        'inventory': 'إدارة المخزون'
    };

    modalBody.innerHTML = `
        <div class="employee-profile">
            <div class="employee-profile-avatar">
                <i class="fas ${roleIcon}"></i>
            </div>
            <div class="employee-profile-info">
                <div class="employee-profile-name">${employee.name}</div>
                <div class="employee-profile-code">${employee.code}</div>
                <div style="margin-top: 8px; display: flex; gap: 8px; flex-wrap: wrap;">
                    <span class="employee-status ${statusClass}" style="font-size: 12px; padding: 4px 12px;">
                        <i class="fas ${statusIcon}"></i> ${statusText}
                    </span>
                    <span class="role-badge" style="background: var(--stat-bg); color: var(--primary); padding: 4px 12px; border-radius: 12px; font-size: 12px;">
                        <i class="fas fa-user-tag"></i> ${employee.roleLabel}
                    </span>
                </div>
            </div>
        </div>

        <div class="details-section">
            <h3 class="section-title"><i class="fas fa-id-card"></i> البيانات الوظيفية</h3>
            <div class="details-grid">
                <div class="detail-item">
                    <span class="detail-label">رقم الموظف</span>
                    <span class="detail-value">${employee.number}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">البريد الإلكتروني</span>
                    <span class="detail-value">${employee.email}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">رقم الهاتف</span>
                    <span class="detail-value">${employee.phone}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">المنصب</span>
                    <span class="detail-value">${employee.roleLabel}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">تاريخ التعيين</span>
                    <span class="detail-value">${formatDate(employee.createdAt)}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">تم الإنشاء بواسطة</span>
                    <span class="detail-value">${employee.createdBy}</span>
                </div>
            </div>
        </div>

        <div class="permissions-section">
            <h3 class="section-title"><i class="fas fa-key"></i> الصلاحيات</h3>
            <div class="permissions-list">
                ${employee.permissions.includes('all') 
                    ? '<span class="permission-badge all"><i class="fas fa-check-double"></i> جميع الصلاحيات</span>'
                    : employee.permissions.map(p => {
                        const labels = {
                            'branches': 'إدارة الفروع',
                            'customers': 'إدارة العملاء',
                            'sales': 'المبيعات',
                            'support': 'الدعم الفني',
                            'inventory': 'إدارة المخزون'
                        };
                        return `<span class="permission-badge"><i class="fas fa-check"></i> ${labels[p] || p}</span>`;
                    }).join('')
                }
            </div>
        </div>

        <div class="actions-section" style="margin: 20px 0; display: flex; gap: 12px;">
            <button class="btn btn-next" onclick="openEditModal(${employee.id})">
                <i class="fas fa-edit"></i>
                <span>تعديل البيانات</span>
            </button>
            <button class="btn btn-prev" onclick="openDeleteModal(${employee.id})" style="border-color: #ff4757; color: #ff4757;">
                <i class="fas fa-trash"></i>
                <span>حذف الموظف</span>
            </button>
        </div>
    `;

    document.getElementById('employeeDetailModal').classList.add('active');
}

// Close detail modal
function closeDetailModal() {
    document.getElementById('employeeDetailModal').classList.remove('active');
}

// Open add employee modal
function openAddModal() {
    document.getElementById('addModal').classList.add('active');
}

// Close add modal
function closeAddModal() {
    document.getElementById('addModal').classList.remove('active');
    document.getElementById('addEmployeeForm').reset();
    resetAddFormIndicators();
}

// Reset add form indicators
function resetAddFormIndicators() {
    const strengthBar = document.getElementById('addStrengthBar');
    if (strengthBar) strengthBar.style.width = '0%';
    const matchIndicator = document.getElementById('addPasswordMatch');
    if (matchIndicator) {
        matchIndicator.textContent = '';
        matchIndicator.className = 'password-match';
    }
}

// Open edit employee modal
function openEditModal(employeeId) {
    const employee = employees.find(e => e.id === employeeId);
    if (!employee) return;

    const modal = document.getElementById('editModal');
    const form = document.getElementById('editEmployeeForm');

    form.dataset.employeeId = employeeId;
    form.elements.employeeName.value = employee.name;
    form.elements.employeeNumber.value = employee.number;
    form.elements.employeeCode.value = employee.code;
    form.elements.employeeEmail.value = employee.email || '';
    form.elements.employeePhone.value = employee.phone || '';
    form.elements.employeeRole.value = employee.role;
    form.elements.employeeStatus.value = employee.status;

    modal.classList.add('active');
    closeDetailModal();
}

// Close edit modal
function closeEditModal() {
    document.getElementById('editModal').classList.remove('active');
}

// Save edited employee
document.getElementById('editEmployeeForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    const employeeId = parseInt(e.target.dataset.employeeId);
    const employee = employees.find(e => e.id === employeeId);
    
    if (!employee) return;

    employee.name = e.target.employeeName.value;
    employee.number = e.target.employeeNumber.value;
    employee.code = e.target.employeeCode.value;
    employee.email = e.target.employeeEmail.value;
    employee.phone = e.target.employeePhone.value;
    employee.role = e.target.employeeRole.value;
    employee.roleLabel = getRoleLabel(e.target.employeeRole.value);
    employee.status = e.target.employeeStatus.value;

    closeEditModal();
    loadEmployees();
    showToast('تم تحديث بيانات الموظف بنجاح');
});

function getRoleLabel(role) {
    const labels = {
        'manager': 'مدير',
        'supervisor': 'مشرف',
        'sales': 'موظف مبيعات',
        'support': 'الدعم الفني',
        'warehouse': 'موظف مخزن'
    };
    return labels[role] || role;
}

// Open delete confirmation modal
function openDeleteModal(employeeId) {
    const employee = employees.find(e => e.id === employeeId);
    if (!employee) return;

    document.getElementById('deleteEmployeeName').textContent = employee.name;
    document.getElementById('deleteModal').dataset.employeeId = employeeId;
    document.getElementById('deleteModal').classList.add('active');
    closeDetailModal();
}

// Close delete modal
function closeDeleteModal() {
    document.getElementById('deleteModal').classList.remove('active');
}

// Confirm delete employee
function confirmDelete() {
    const modal = document.getElementById('deleteModal');
    const employeeId = parseInt(modal.dataset.employeeId);
    
    employees = employees.filter(e => e.id !== employeeId);
    
    closeDeleteModal();
    loadEmployees();
    updateStats();
    showToast('تم حذف الموظف بنجاح');
}

// Filter employees
function filterEmployees() {
    const searchTerm = document.getElementById('searchInput').value.toLowerCase();
    const roleFilter = document.getElementById('roleFilter').value;
    const statusFilter = document.getElementById('statusFilter').value;

    const filtered = employees.filter(employee => {
        const matchesSearch = employee.name.toLowerCase().includes(searchTerm) ||
                            employee.code.toLowerCase().includes(searchTerm) ||
                            employee.number.toLowerCase().includes(searchTerm) ||
                            employee.phone.includes(searchTerm);
        const matchesRole = roleFilter === 'all' || employee.role === roleFilter;
        const matchesStatus = statusFilter === 'all' || employee.status === statusFilter;
        return matchesSearch && matchesRole && matchesStatus;
    });

    loadEmployees(filtered);
}

// Update statistics
function updateStats() {
    document.getElementById('totalCount').textContent = employees.length;
    document.getElementById('activeCount').textContent = employees.filter(e => e.status === 'active').length;
}

// Add new employee
document.getElementById('addEmployeeForm').addEventListener('submit', function(e) {
    e.preventDefault();

    const formData = new FormData(e.target);
    const newEmployee = {
        id: employees.length > 0 ? Math.max(...employees.map(e => e.id)) + 1 : 1,
        name: formData.get('employeeName'),
        number: formData.get('employeeNumber'),
        code: formData.get('employeeCode'),
        email: formData.get('employeeEmail'),
        phone: formData.get('employeePhone'),
        role: formData.get('employeeRole'),
        roleLabel: getRoleLabel(formData.get('employeeRole')),
        status: 'active',
        createdAt: new Date().toISOString().split('T')[0],
        createdBy: "المستخدم الحالي",
        permissions: getDefaultPermissions(formData.get('employeeRole'))
    };

    employees.unshift(newEmployee);
    updateStats();
    loadEmployees();
    closeAddModal();
    showToast('تم إضافة الموظف بنجاح');
});

function getDefaultPermissions(role) {
    const permissionsMap = {
        'manager': ['all'],
        'supervisor': ['branches', 'customers', 'reports'],
        'sales': ['sales', 'customers'],
        'support': ['support'],
        'warehouse': ['inventory']
    };
    return permissionsMap[role] || [];
}

// Initialize modals
function initializeModals() {
    document.querySelectorAll('.modal-overlay').forEach(overlay => {
        overlay.addEventListener('click', function(e) {
            if (e.target === this) {
                this.classList.remove('active');
            }
        });
    });

    document.getElementById('toastClose').addEventListener('click', hideToast);
}

// Toast notification
function showToast(message) {
    const toast = document.getElementById('successToast');
    const toastMessage = document.getElementById('toastMessage');
    if (toast && toastMessage) {
        toastMessage.textContent = message;
        toast.classList.add('show');
        setTimeout(() => hideToast(), 3000);
    }
}

function hideToast() {
    const toast = document.getElementById('successToast');
    if (toast) {
        toast.classList.remove('show');
    }
}

// Password visibility toggle (for add form)
window.togglePasswordVisibility = function(prefix) {
    const passwordField = document.getElementById(prefix);
    if (!passwordField) return;
    
    const toggleBtn = passwordField.parentElement.querySelector('.password-toggle');
    if (!toggleBtn) return;
    
    const icon = toggleBtn.querySelector('i');
    if (passwordField.type === 'password') {
        passwordField.type = 'text';
        icon.classList.remove('fa-eye');
        icon.classList.add('fa-eye-slash');
    } else {
        passwordField.type = 'password';
        icon.classList.remove('fa-eye-slash');
        icon.classList.add('fa-eye');
    }
};

// Format date helper
function formatDate(dateString) {
    const date = new Date(dateString);
    return date.toLocaleDateString('ar-EG', { year: 'numeric', month: 'short', day: 'numeric' });
}
