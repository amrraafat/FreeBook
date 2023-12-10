function Delete(id) {
    Swal.fire({
        title: "هل انتا متأكد ؟",
        text: "لن تتمكن من التراجع عن هذا!!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = "/Admin/Accounts/DeleteRoles?Id=" +id ;
            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
        }
    });
}

Edit = (id, name) =>
{
    document.getElementById("titel").innerHTML = "تم تعديل مجموعة المستخدم بنجاح";
    document.getElementById("btnSave").innerHTML = "تعديل";
    document.getElementById("roleId").value = id;
    document.getElementById("roleName").value = name;
}

Rest = (id, name) => {
    document.getElementById("titel").innerHTML = "اضف مجموعة جديدة";
    document.getElementById("btnSave").innerHTML = "حفظ";
    document.getElementById("roleId").value = id;
    document.getElementById("roleName").value = name;
}