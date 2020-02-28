<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RtBoxCC.ascx.cs" Inherits="Controles_RtBoxCC" %>
<meta http-equiv="X-UA-Compatible" content="IE=8;FF=3;OtherUA=4" />
<script type="text/javascript">
    tinymce.init({
        statusbar: true,
        nonbreaking_force_tab: true,
        height: 300,
        width: 1100,
        min_width: 600,
        menu: { // this is the complete default configuration
            edit: { title: 'Editar', items: 'undo redo | cut copy paste pastetext | selectall' },
            format: { title: 'Formato', items: 'bold italic underline strikethrough superscript subscript | formats | removeformat' },
            table: { title: 'Tabla', items: 'inserttable tableprops deletetable | cell row column' }

        },
        selector: "textarea",
        mode: "textareas",
        language: 'es',
        language_url: '../Scripts/tinymce/langs/langs/es.js',
        nowrap: false,

        plugins: [
    "advlist autolink lists link image charmap print preview anchor",
    "searchreplace visualblocks code fullscreen hr textcolor ",
    "insertdatetime media table contextmenu paste nonbreaking directionality "
        ],
        toolbar: ["undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent ",
                    "hr charmap | insertdatetime | nonbreaking | ltr rtl | forecolor backcolor"
        ]
    });
</script>

<textarea id="txttextarea" name="mytextarea" rows="15" cols="120" style="width: 98%" runat="server"/>
