// Sample employees data (shared between pages)
const employeesData = [
    {
        id: 1,
        name: "أحمد محمد علي",
        number: "EMP-001",
        code: "ahmed001",
        email: "ahmed@engas.com",
        phone: "010-1234-5678",
        role: "manager",
        roleLabel: "مدير",
        status: "active",
        createdAt: "2025-06-01",
        createdBy: "النظام",
        permissions: ["all"]
    },
    {
        id: 2,
        name: "فاطمة حسن السيد",
        number: "EMP-002",
        code: "fatima002",
        email: "fatima@engas.com",
        phone: "011-5678-9012",
        role: "supervisor",
        roleLabel: "مشرف",
        status: "active",
        createdAt: "2025-07-15",
        createdBy: "أحمد محمد",
        permissions: ["branches", "customers", "reports"]
    },
    {
        id: 3,
        name: "محمد سعيد إبراهيم",
        number: "EMP-003",
        code: "mohamed003",
        email: "mohamed@engas.com",
        phone: "012-9012-3456",
        role: "sales",
        roleLabel: "موظف مبيعات",
        status: "active",
        createdAt: "2025-08-20",
        createdBy: "أحمد محمد",
        permissions: ["sales", "customers"]
    },
    {
        id: 4,
        name: "نور الدين أحمد",
        number: "EMP-004",
        code: "noor004",
        email: "noor@engas.com",
        phone: "010-3456-7890",
        role: "support",
        roleLabel: "الدعم الفني",
        status: "inactive",
        createdAt: "2025-09-10",
        createdBy: "فاطمة حسن",
        permissions: ["support"]
    },
    {
        id: 5,
        name: "سارة محمود حسن",
        number: "EMP-005",
        code: "sara005",
        email: "sara@engas.com",
        phone: "011-7890-1234",
        role: "warehouse",
        roleLabel: "موظف مخزن",
        status: "active",
        createdAt: "2025-10-05",
        createdBy: "أحمد محمد",
        permissions: ["inventory"]
    },
    {
        id: 6,
        name: "عمر يوسف كامل",
        number: "EMP-006",
        code: "omar006",
        email: "omar@engas.com",
        phone: "012-2345-6789",
        role: "sales",
        roleLabel: "موظف مبيعات",
        status: "active",
        createdAt: "2025-11-12",
        createdBy: "فاطمة حسن",
        permissions: ["sales", "customers"]
    },
    {
        id: 7,
        name: "حسن علي عبدالله",
        number: "EMP-007",
        code: "hassan007",
        email: "hassan@engas.com",
        phone: "010-4567-8901",
        role: "warehouse",
        roleLabel: "موظف مخزن",
        status: "active",
        createdAt: "2025-12-01",
        createdBy: "سارة محمود",
        permissions: ["inventory"]
    }
];

// Make globally available
window.employees = [...employeesData];
