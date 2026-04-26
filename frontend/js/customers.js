// Sample customer data (in real app, this would come from backend)
let customers = [
    {
        id: 1,
        name: "أحمد محمد علي",
        code: "CUST-001",
        phone: "010-1234-5678",
        email: "ahmed@example.com",
        governorate: "القاهرة",
        address: "مدينة نصر - شارع الجيش",
        status: "active",
        createdAt: "2026-01-15",
        purchases: [
            { 
                id: 1,
                branch: "فرع المهندسين", 
                branchCode: "BR-001",
                items: [
                    { product: "iPhone 15 Pro", category: "هواتف", quantity: 1, price: 45000, total: 45000 },
                    { product: "Samsung Galaxy S24", category: "هواتف", quantity: 1, price: 38000, total: 38000 },
                    { product: "شاحن سريع 65W", category: "إكسسوارات", quantity: 2, price: 800, total: 1600 }
                ],
                paymentMethod: "بطاقة ائتمان", 
                totalAmount: 84600, 
                date: "2026-01-20",
                time: "منذ 3 ساعات"
            },
            { 
                id: 2,
                branch: "فرع مصر الجديدة", 
                branchCode: "BR-003",
                items: [
                    { product: "MacBook Pro 16\"", category: "أجهزة لابتوب", quantity: 1, price: 55000, total: 55000 },
                    { product: "Apple AirPods Pro", category: "إكسسوارات", quantity: 1, price: 4000, total: 4000 }
                ],
                paymentMethod: "تحويل بنكي", 
                totalAmount: 59000, 
                date: "2026-01-18",
                time: "منذ 3 أيام"
            }
        ]
    },
    {
        id: 2,
        name: "فاطمة حسن السيد",
        code: "CUST-002",
        phone: "011-5678-9012",
        email: "fatima@example.com",
        governorate: "الجيزة",
        address: "فيصل - شارع الملك فيصل",
        status: "active",
        createdAt: "2026-01-20",
        purchases: [
            { 
                id: 3,
                branch: "فرع 6 أكتوبر", 
                branchCode: "BR-002",
                items: [
                    { product: "Sony WH-1000XM5", category: "سماعات", quantity: 1, price: 6000, total: 6000 },
                    { product: "iPad Air", category: "أجهزة لوحية", quantity: 1, price: 18000, total: 18000 }
                ],
                paymentMethod: "نقدي", 
                totalAmount: 24000, 
                date: "2026-01-25",
                time: "منذ 5 ساعات"
            }
        ]
    },
    {
        id: 3,
        name: "محمد سعيد إبراهيم",
        code: "CUST-003",
        phone: "012-9012-3456",
        email: "mohamed@example.com",
        governorate: "الإسكندرية",
        address: "سموحة - شارع الملك فيصل",
        status: "inactive",
        createdAt: "2026-02-01",
        purchases: [
            { 
                id: 4,
                branch: "فرع سموحة", 
                branchCode: "BR-005",
                items: [
                    { product: "Dell XPS 15", category: "أجهزة لابتوب", quantity: 1, price: 42000, total: 42000 }
                ],
                paymentMethod: "تحويل بنكي", 
                totalAmount: 42000, 
                date: "2026-02-05",
                time: "منذ أسبوعين"
            }
        ]
    },
    {
        id: 4,
        name: "نور الدين أحمد",
        code: "CUST-004",
        phone: "010-3456-7890",
        email: "noor@example.com",
        governorate: "الدقهلية",
        address: "المنصورة - شارع الجيش",
        status: "pending",
        createdAt: "2026-02-10",
        purchases: []
    },
    {
        id: 5,
        name: "سارة محمود حسن",
        code: "CUST-005",
        phone: "011-7890-1234",
        email: "sara@example.com",
        governorate: "القاهرة",
        address: "مصر الجديدة - شارع العروبة",
        status: "active",
        createdAt: "2026-02-15",
        purchases: [
            { 
                id: 5,
                branch: "فرع التجمع", 
                branchCode: "BR-004",
                items: [
                    { product: "Samsung 65\" QLED", category: "تليفزيونات", quantity: 1, price: 35000, total: 35000 },
                    { product: "LG OLED 55\"", category: "تليفزيونات", quantity: 1, price: 28000, total: 28000 },
                    { product: "Home Theater System", category: "صوت", quantity: 1, price: 8500, total: 8500 }
                ],
                paymentMethod: "بطاقة ائتمان", 
                totalAmount: 71500, 
                date: "2026-02-20",
                time: "منذ 4 أيام"
            }
        ]
    },
    {
        id: 6,
        name: "عمر يوسف كامل",
        code: "CUST-006",
        phone: "012-2345-6789",
        email: "omar@example.com",
        governorate: "الإسماعيلية",
        address: "الإسماعيلية - حي 3",
        status: "active",
        createdAt: "2026-02-20",
        purchases: [
            { 
                id: 6,
                branch: "فرع الإسماعيلية", 
                branchCode: "BR-006",
                items: [
                    { product: "Xiaomi Redmi Note 13", category: "هواتف", quantity: 2, price: 8000, total: 16000 },
                    { product: "شاحن لاسلكي", category: "إكسسوارات", quantity: 2, price: 600, total: 1200 }
                ],
                paymentMethod: "تقسيط", 
                totalAmount: 20000, 
                date: "2026-02-28",
                time: "منذ 6 أيام"
            }
        ]
    }
];

// Product categories mapping
const productCategories = {
    'هواتف': 'fas fa-mobile-alt',
    'أجهزة لابتوب': 'fas fa-laptop',
    'أجهزة لوحية': 'fas fa-tablet-alt',
    'تليفزيونات': 'fas fa-tv',
    'سماعات': 'fas fa-headphones',
    'إكسسوارات': 'fas fa-plug',
    'صوت': 'fas fa-speaker'
};

// Initialize the page
document.addEventListener('DOMContentLoaded', () => {
    loadCustomers();
    updateStats();
    initializeModals();
});

// Load customers into grid
function loadCustomers(data = customers) {
    const grid = document.getElementById('customersGrid');
    grid.innerHTML = '';

    if (data.length === 0) {
        document.getElementById('noResults').style.display = 'flex';
        return;
    }

    document.getElementById('noResults').style.display = 'none';

    data.forEach((customer, index) => {
        const card = createCustomerCard(customer);
        card.style.animationDelay = `${index * 0.05}s`;
        grid.appendChild(card);
    });
}

// Create customer card element
function createCustomerCard(customer) {
    const card = document.createElement('div');
    card.className = 'customer-card';
    card.onclick = () => openCustomerModal(customer.id);

    const statusClass = customer.status === 'active' ? 'status-active' :
                       customer.status === 'inactive' ? 'status-inactive' : 'status-pending';
    const statusIcon = customer.status === 'active' ? 'fa-check-circle' :
                      customer.status === 'inactive' ? 'fa-times-circle' : 'fa-clock';
    const statusText = customer.status === 'active' ? 'نشط' :
                      customer.status === 'inactive' ? 'غير نشط' : 'معلق';

    const initials = customer.name.split(' ').map(n => n[0]).join('').substring(0, 2);

    card.innerHTML = `
        <div class="customer-card-header">
            <div class="customer-avatar">${initials}</div>
            <div class="customer-info">
                <div class="customer-name">${customer.name}</div>
                <div class="customer-code">${customer.code}</div>
            </div>
            <div class="customer-status ${statusClass}">
                <i class="fas ${statusIcon}"></i>
                ${statusText}
            </div>
            <div class="customer-actions">
                <button class="action-btn-icon edit-btn" onclick="event.stopPropagation(); openEditModal(${customer.id})" title="تعديل">
                    <i class="fas fa-edit"></i>
                </button>
                <button class="action-btn-icon delete-btn" onclick="event.stopPropagation(); openDeleteModal(${customer.id})" title="حذف">
                    <i class="fas fa-trash"></i>
                </button>
            </div>
        </div>
        <div class="customer-details">
            <div class="detail-row">
                <i class="fas fa-phone"></i>
                <span>${customer.phone}</span>
            </div>
            <div class="detail-row">
                <i class="fas fa-map-marker-alt"></i>
                <span>${customer.governorate} - ${customer.address}</span>
            </div>
        </div>
        <div class="customer-meta">
            <div class="meta-item">
                <i class="fas fa-calendar"></i>
                <span>${formatDate(customer.createdAt)}</span>
            </div>
            <div class="meta-item">
                <i class="fas fa-shopping-cart"></i>
                <span>${customer.purchases.length} عملية شراء</span>
            </div>
        </div>
    `;

    return card;
}

// Open customer detail modal
function openCustomerModal(customerId) {
    const customer = customers.find(c => c.id === customerId);
    if (!customer) return;

    const modalBody = document.getElementById('modalBody');
    const initials = customer.name.split(' ').map(n => n[0]).join('').substring(0, 2);

    const statusClass = customer.status === 'active' ? 'status-active' :
                       customer.status === 'inactive' ? 'status-inactive' : 'status-pending';
    const statusText = customer.status === 'active' ? 'نشط' :
                      customer.status === 'inactive' ? 'غير نشط' : 'معلق';

    modalBody.innerHTML = `
        <div class="customer-profile">
            <div class="customer-profile-avatar">${initials}</div>
            <div class="customer-profile-info">
                <div class="customer-profile-name">${customer.name}</div>
                <div class="customer-profile-code">${customer.code}</div>
                <div style="margin-top: 8px;">
                    <span class="customer-status ${statusClass}" style="font-size: 12px; padding: 4px 10px;">
                        <i class="fas ${customer.status === 'active' ? 'fa-check-circle' : customer.status === 'inactive' ? 'fa-times-circle' : 'fa-clock'}"></i>
                        ${statusText}
                    </span>
                </div>
            </div>
        </div>

        <div class="details-section">
            <h3 class="section-title"><i class="fas fa-info-circle"></i> المعلومات الأساسية</h3>
            <div class="details-grid">
                <div class="detail-item">
                    <span class="detail-label">رقم الهاتف</span>
                    <span class="detail-value">${customer.phone}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">البريد الإلكتروني</span>
                    <span class="detail-value">${customer.email || 'غير محدد'}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">المحافظة</span>
                    <span class="detail-value">${customer.governorate}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">العنوان</span>
                    <span class="detail-value">${customer.address}</span>
                </div>
                <div class="detail-item">
                    <span class="detail-label">تاريخ التسجيل</span>
                    <span class="detail-value">${formatDate(customer.createdAt)}</span>
                </div>
            </div>
        </div>

        <div class="actions-section" style="margin: 20px 0; display: flex; gap: 12px;">
            <button class="btn btn-next" onclick="openEditModal(${customer.id})">
                <i class="fas fa-edit"></i>
                <span>تعديل البيانات</span>
            </button>
            <button class="btn btn-prev" onclick="openDeleteModal(${customer.id})" style="border-color: #ff4757; color: #ff4757;">
                <i class="fas fa-trash"></i>
                <span>حذف العميل</span>
            </button>
        </div>

        <div class="operations-section">
            <h3 class="section-title"><i class="fas fa-shopping-cart"></i> سجل عمليات الشراء</h3>
            <div class="purchases-list">
                ${customer.purchases.length > 0 ? customer.purchases.map(purchase => `
                    <div class="purchase-card">
                        <div class="purchase-header">
                            <div class="purchase-branch">
                                <i class="fas fa-building"></i>
                                <span>${purchase.branch}</span>
                                <span class="branch-code">[${purchase.branchCode}]</span>
                            </div>
                            <div class="purchase-meta">
                                <span class="purchase-date">
                                    <i class="fas fa-calendar"></i> ${formatDate(purchase.date)}
                                </span>
                                <span class="purchase-time">${purchase.time}</span>
                            </div>
                        </div>
                        
                        <div class="purchase-items">
                            <h4 class="items-title"><i class="fas fa-box-open"></i> المنتجات</h4>
                            ${purchase.items.map(item => {
                                const iconClass = productCategories[item.category] || 'fas fa-box';
                                return `
                                <div class="purchase-item">
                                    <div class="item-icon">
                                        <i class="${iconClass}"></i>
                                    </div>
                                    <div class="item-details">
                                        <div class="item-name">${item.product}</div>
                                        <div class="item-meta">
                                            <span class="item-category">${item.category}</span>
                                            <span class="item-qty">العدد: ${item.quantity}</span>
                                        </div>
                                    </div>
                                    <div class="item-price">
                                        ${formatCurrency(item.total)}
                                    </div>
                                </div>
                                `;
                            }).join('')}
                        </div>

                        <div class="purchase-footer">
                            <div class="purchase-payment">
                                <span class="payment-label">طريقة الدفع:</span>
                                <span class="payment-method">${purchase.paymentMethod}</span>
                            </div>
                            <div class="purchase-total">
                                <span class="total-label">الإجمالي:</span>
                                <span class="total-amount">${formatCurrency(purchase.totalAmount)}</span>
                            </div>
                        </div>
                    </div>
                `).join('') : '<div class="no-purchases"><i class="fas fa-shopping-cart"></i><h4>لا توجد عمليات شراء</h4><p>هذا العميل لم يقم بأي عملية شراء بعد</p></div>'}
            </div>
        </div>
    `;

    document.getElementById('customerModal').classList.add('active');
}

// Close modal
function closeModal() {
    document.getElementById('customerModal').classList.remove('active');
}

// Open add customer modal
function openAddModal() {
    document.getElementById('addModal').classList.add('active');
}

// Close add modal
function closeAddModal() {
    document.getElementById('addModal').classList.remove('active');
    document.getElementById('addCustomerForm').reset();
}

// Open edit customer modal
function openEditModal(customerId) {
    const customer = customers.find(c => c.id === customerId);
    if (!customer) return;

    const modal = document.getElementById('editModal');
    const form = document.getElementById('editCustomerForm');

    form.dataset.customerId = customerId;
    form.elements.customerName.value = customer.name;
    form.elements.customerCode.value = customer.code;
    form.elements.customerPhone.value = customer.phone;
    form.elements.customerEmail.value = customer.email || '';
    form.elements.customerGovernorate.value = customer.governorate;
    form.elements.customerAddress.value = customer.address || '';
    form.elements.customerStatus.value = customer.status;

    modal.classList.add('active');
    closeModal(); // Close view modal if open
}

// Close edit modal
function closeEditModal() {
    document.getElementById('editModal').classList.remove('active');
}

// Save edited customer
document.getElementById('editCustomerForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    const customerId = parseInt(e.target.dataset.customerId);
    const customer = customers.find(c => c.id === customerId);
    
    if (!customer) return;

    customer.name = e.target.customerName.value;
    customer.code = e.target.customerCode.value;
    customer.phone = e.target.customerPhone.value;
    customer.email = e.target.customerEmail.value;
    customer.governorate = e.target.customerGovernorate.value;
    customer.address = e.target.customerAddress.value;
    customer.status = e.target.customerStatus.value;

    closeEditModal();
    loadCustomers();
    showToast('تم تحديث بيانات العميل بنجاح');
});

// Open delete confirmation modal
function openDeleteModal(customerId) {
    const customer = customers.find(c => c.id === customerId);
    if (!customer) return;

    document.getElementById('deleteCustomerName').textContent = customer.name;
    document.getElementById('deleteModal').dataset.customerId = customerId;
    document.getElementById('deleteModal').classList.add('active');
    closeModal(); // Close view modal if open
}

// Close delete modal
function closeDeleteModal() {
    document.getElementById('deleteModal').classList.remove('active');
}

// Confirm delete customer
function confirmDelete() {
    const modal = document.getElementById('deleteModal');
    const customerId = parseInt(modal.dataset.customerId);
    
    customers = customers.filter(c => c.id !== customerId);
    
    closeDeleteModal();
    loadCustomers();
    updateStats();
    showToast('تم حذف العميل بنجاح');
}

// Filter customers
function filterCustomers() {
    const searchTerm = document.getElementById('searchInput').value.toLowerCase();
    const statusFilter = document.getElementById('statusFilter').value;

    const filtered = customers.filter(customer => {
        const matchesSearch = customer.name.toLowerCase().includes(searchTerm) ||
                            customer.code.toLowerCase().includes(searchTerm) ||
                            customer.phone.includes(searchTerm);
        const matchesStatus = statusFilter === 'all' || customer.status === statusFilter;
        return matchesSearch && matchesStatus;
    });

    loadCustomers(filtered);
}

// Sort customers (placeholder)
function toggleSortMenu() {
    showToast('خيار الترتيب قيد التطوير');
}

// Update statistics
function updateStats() {
    document.getElementById('totalCount').textContent = customers.length;
    document.getElementById('activeCount').textContent = customers.filter(c => c.status === 'active').length;
}

// Add new customer
document.getElementById('addCustomerForm').addEventListener('submit', function(e) {
    e.preventDefault();

    const formData = new FormData(e.target);
    const newCustomer = {
        id: customers.length > 0 ? Math.max(...customers.map(c => c.id)) + 1 : 1,
        name: formData.get('customerName'),
        code: formData.get('customerCode'),
        phone: formData.get('customerPhone'),
        email: formData.get('customerEmail'),
        governorate: formData.get('customerGovernorate'),
        address: formData.get('customerAddress') || 'غير محدد',
        status: 'pending',
        createdAt: new Date().toISOString().split('T')[0],
        purchases: []
    };

    customers.unshift(newCustomer);
    updateStats();
    loadCustomers();
    closeAddModal();
    showToast('تم إضافة العميل بنجاح');
});

// Initialize modals event handlers
function initializeModals() {
    // Close modals on outside click
    document.querySelectorAll('.modal-overlay').forEach(overlay => {
        overlay.addEventListener('click', function(e) {
            if (e.target === this) {
                this.classList.remove('active');
            }
        });
    });

    // Close buttons
    document.getElementById('toastClose').addEventListener('click', hideToast);
}

// Toast notification
function showToast(message) {
    const toast = document.getElementById('successToast');
    document.getElementById('toastMessage').textContent = message;
    toast.classList.add('show');
    setTimeout(() => hideToast(), 3000);
}

function hideToast() {
    document.getElementById('successToast').classList.remove('show');
}

// Format date helper
function formatDate(dateString) {
    const date = new Date(dateString);
    return date.toLocaleDateString('ar-EG', { year: 'numeric', month: 'short', day: 'numeric' });
}

// Format currency helper
function formatCurrency(amount) {
    return amount.toLocaleString('ar-EG') + ' ج.م';
}
