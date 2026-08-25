<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Cobrando
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.panelDatos = New System.Windows.Forms.Panel()
        Me.tlpDatos = New System.Windows.Forms.TableLayoutPanel()
        Me.picClienteFoto = New System.Windows.Forms.PictureBox()
        Me.panelDatosCliente = New System.Windows.Forms.Panel()
        Me.labelClienteClave = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.labelClienteNombre = New System.Windows.Forms.Label()
        Me.panelMonederoElectronico = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.label_ME_Disponible = New System.Windows.Forms.Label()
        Me.label_ME_ImporteEstaVenta = New System.Windows.Forms.Label()
        Me.label_ME_label = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.panelCreditoSuperventas = New System.Windows.Forms.Panel()
        Me.labelCreditoUsado = New System.Windows.Forms.Label()
        Me.labelCreditoDisponible_ = New System.Windows.Forms.Label()
        Me.labelCreditoDisponible = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.labelCreditoLimite = New System.Windows.Forms.Label()
        Me.labelCreditoLimite_ = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnTerminar = New System.Windows.Forms.Button()
        Me.btnTerminarEImprimir = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.tlpImportes = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.labelImporteVenta_Saldo = New System.Windows.Forms.Label()
        Me.boxImporteVenta = New System.Windows.Forms.Label()
        Me.labelCobrandoME = New System.Windows.Forms.Label()
        Me.labelCobrandoCS = New System.Windows.Forms.Label()
        Me.txtCobrandoCredito = New System.Windows.Forms.TextBox()
        Me.labelImportePagoCliente_TOTAL = New System.Windows.Forms.Label()
        Me.labelCobrandoEfectivoRecibido = New System.Windows.Forms.Label()
        Me.labelAdeudoAPagar = New System.Windows.Forms.Label()
        Me.labelVuelto_SaldoNuevo = New System.Windows.Forms.Label()
        Me.boxVuelto = New System.Windows.Forms.Label()
        Me.boxImportePagoCliente_TOTAL = New System.Windows.Forms.Label()
        Me.btnPendientes = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtCobrandoME = New System.Windows.Forms.TextBox()
        Me.txtAdeudoAPagar = New System.Windows.Forms.TextBox()
        Me.txtCobrandoEfectivoRecibido = New System.Windows.Forms.TextBox()
        Me.labelEfectivoRecibido = New System.Windows.Forms.Label()
        Me.panelCapturaImportes = New System.Windows.Forms.Panel()
        Me.panelCaptura = New System.Windows.Forms.Panel()
        Me.timerParpadeaAdeudo = New System.Windows.Forms.Timer(Me.components)
        Me.timerParpadeaLables = New System.Windows.Forms.Timer(Me.components)
        Me.panelDatos.SuspendLayout()
        Me.tlpDatos.SuspendLayout()
        CType(Me.picClienteFoto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelDatosCliente.SuspendLayout()
        Me.panelMonederoElectronico.SuspendLayout()
        Me.panelCreditoSuperventas.SuspendLayout()
        Me.tlpImportes.SuspendLayout()
        Me.panelCapturaImportes.SuspendLayout()
        Me.panelCaptura.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelDatos
        '
        Me.panelDatos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panelDatos.BackColor = System.Drawing.Color.Transparent
        Me.panelDatos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panelDatos.Controls.Add(Me.tlpDatos)
        Me.panelDatos.Location = New System.Drawing.Point(11, 12)
        Me.panelDatos.Name = "panelDatos"
        Me.panelDatos.Size = New System.Drawing.Size(406, 475)
        Me.panelDatos.TabIndex = 0
        '
        'tlpDatos
        '
        Me.tlpDatos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpDatos.ColumnCount = 1
        Me.tlpDatos.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpDatos.Controls.Add(Me.picClienteFoto, 0, 0)
        Me.tlpDatos.Controls.Add(Me.panelDatosCliente, 0, 1)
        Me.tlpDatos.Controls.Add(Me.panelMonederoElectronico, 0, 2)
        Me.tlpDatos.Controls.Add(Me.panelCreditoSuperventas, 0, 3)
        Me.tlpDatos.Location = New System.Drawing.Point(3, 1)
        Me.tlpDatos.Name = "tlpDatos"
        Me.tlpDatos.RowCount = 4
        Me.tlpDatos.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpDatos.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.tlpDatos.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpDatos.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpDatos.Size = New System.Drawing.Size(396, 467)
        Me.tlpDatos.TabIndex = 14
        '
        'picClienteFoto
        '
        Me.picClienteFoto.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picClienteFoto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picClienteFoto.Location = New System.Drawing.Point(3, 3)
        Me.picClienteFoto.Name = "picClienteFoto"
        Me.picClienteFoto.Size = New System.Drawing.Size(390, 193)
        Me.picClienteFoto.TabIndex = 8
        Me.picClienteFoto.TabStop = False
        '
        'panelDatosCliente
        '
        Me.panelDatosCliente.BackColor = System.Drawing.Color.White
        Me.panelDatosCliente.Controls.Add(Me.labelClienteClave)
        Me.panelDatosCliente.Controls.Add(Me.Label5)
        Me.panelDatosCliente.Controls.Add(Me.Label4)
        Me.panelDatosCliente.Controls.Add(Me.labelClienteNombre)
        Me.panelDatosCliente.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelDatosCliente.Location = New System.Drawing.Point(3, 202)
        Me.panelDatosCliente.Name = "panelDatosCliente"
        Me.panelDatosCliente.Size = New System.Drawing.Size(390, 74)
        Me.panelDatosCliente.TabIndex = 14
        '
        'labelClienteClave
        '
        Me.labelClienteClave.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labelClienteClave.AutoSize = True
        Me.labelClienteClave.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelClienteClave.ForeColor = System.Drawing.Color.Red
        Me.labelClienteClave.Location = New System.Drawing.Point(73, 9)
        Me.labelClienteClave.Name = "labelClienteClave"
        Me.labelClienteClave.Size = New System.Drawing.Size(62, 24)
        Me.labelClienteClave.TabIndex = 24
        Me.labelClienteClave.Text = "Clave"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(4, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 15)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "NOMBRE:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(22, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 15)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "CLAVE:"
        '
        'labelClienteNombre
        '
        Me.labelClienteNombre.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labelClienteNombre.BackColor = System.Drawing.Color.Red
        Me.labelClienteNombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelClienteNombre.ForeColor = System.Drawing.Color.Yellow
        Me.labelClienteNombre.Location = New System.Drawing.Point(73, 33)
        Me.labelClienteNombre.Name = "labelClienteNombre"
        Me.labelClienteNombre.Size = New System.Drawing.Size(314, 34)
        Me.labelClienteNombre.TabIndex = 6
        Me.labelClienteNombre.Text = "Nombre"
        '
        'panelMonederoElectronico
        '
        Me.panelMonederoElectronico.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panelMonederoElectronico.BackColor = System.Drawing.Color.OldLace
        Me.panelMonederoElectronico.Controls.Add(Me.Label1)
        Me.panelMonederoElectronico.Controls.Add(Me.label_ME_Disponible)
        Me.panelMonederoElectronico.Controls.Add(Me.label_ME_ImporteEstaVenta)
        Me.panelMonederoElectronico.Controls.Add(Me.label_ME_label)
        Me.panelMonederoElectronico.Controls.Add(Me.Label7)
        Me.panelMonederoElectronico.Location = New System.Drawing.Point(3, 282)
        Me.panelMonederoElectronico.Name = "panelMonederoElectronico"
        Me.panelMonederoElectronico.Size = New System.Drawing.Size(390, 87)
        Me.panelMonederoElectronico.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(3, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(187, 15)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "MONEDERO ELECTRONICO"
        '
        'label_ME_Disponible
        '
        Me.label_ME_Disponible.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_ME_Disponible.ForeColor = System.Drawing.Color.Blue
        Me.label_ME_Disponible.Location = New System.Drawing.Point(148, 25)
        Me.label_ME_Disponible.Name = "label_ME_Disponible"
        Me.label_ME_Disponible.Size = New System.Drawing.Size(176, 27)
        Me.label_ME_Disponible.TabIndex = 12
        Me.label_ME_Disponible.Text = "0.00"
        Me.label_ME_Disponible.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label_ME_ImporteEstaVenta
        '
        Me.label_ME_ImporteEstaVenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_ME_ImporteEstaVenta.ForeColor = System.Drawing.Color.Blue
        Me.label_ME_ImporteEstaVenta.Location = New System.Drawing.Point(147, 53)
        Me.label_ME_ImporteEstaVenta.Name = "label_ME_ImporteEstaVenta"
        Me.label_ME_ImporteEstaVenta.Size = New System.Drawing.Size(177, 26)
        Me.label_ME_ImporteEstaVenta.TabIndex = 10
        Me.label_ME_ImporteEstaVenta.Text = "0.00"
        Me.label_ME_ImporteEstaVenta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label_ME_label
        '
        Me.label_ME_label.AutoSize = True
        Me.label_ME_label.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label_ME_label.ForeColor = System.Drawing.Color.Black
        Me.label_ME_label.Location = New System.Drawing.Point(18, 55)
        Me.label_ME_label.Name = "label_ME_label"
        Me.label_ME_label.Size = New System.Drawing.Size(126, 18)
        Me.label_ME_label.TabIndex = 9
        Me.label_ME_label.Text = "Por esta Compra:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(63, 29)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(81, 18)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Disponible:"
        '
        'panelCreditoSuperventas
        '
        Me.panelCreditoSuperventas.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panelCreditoSuperventas.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.panelCreditoSuperventas.Controls.Add(Me.labelCreditoUsado)
        Me.panelCreditoSuperventas.Controls.Add(Me.labelCreditoDisponible_)
        Me.panelCreditoSuperventas.Controls.Add(Me.labelCreditoDisponible)
        Me.panelCreditoSuperventas.Controls.Add(Me.Label2)
        Me.panelCreditoSuperventas.Controls.Add(Me.labelCreditoLimite)
        Me.panelCreditoSuperventas.Controls.Add(Me.labelCreditoLimite_)
        Me.panelCreditoSuperventas.Controls.Add(Me.Label12)
        Me.panelCreditoSuperventas.Location = New System.Drawing.Point(3, 375)
        Me.panelCreditoSuperventas.Name = "panelCreditoSuperventas"
        Me.panelCreditoSuperventas.Size = New System.Drawing.Size(390, 89)
        Me.panelCreditoSuperventas.TabIndex = 16
        '
        'labelCreditoUsado
        '
        Me.labelCreditoUsado.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelCreditoUsado.ForeColor = System.Drawing.Color.Blue
        Me.labelCreditoUsado.Location = New System.Drawing.Point(164, 35)
        Me.labelCreditoUsado.Name = "labelCreditoUsado"
        Me.labelCreditoUsado.Size = New System.Drawing.Size(159, 30)
        Me.labelCreditoUsado.TabIndex = 10
        Me.labelCreditoUsado.Text = "0.00"
        Me.labelCreditoUsado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'labelCreditoDisponible_
        '
        Me.labelCreditoDisponible_.AutoSize = True
        Me.labelCreditoDisponible_.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelCreditoDisponible_.ForeColor = System.Drawing.Color.Black
        Me.labelCreditoDisponible_.Location = New System.Drawing.Point(63, 63)
        Me.labelCreditoDisponible_.Name = "labelCreditoDisponible_"
        Me.labelCreditoDisponible_.Size = New System.Drawing.Size(81, 18)
        Me.labelCreditoDisponible_.TabIndex = 24
        Me.labelCreditoDisponible_.Text = "Disponible:"
        '
        'labelCreditoDisponible
        '
        Me.labelCreditoDisponible.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelCreditoDisponible.ForeColor = System.Drawing.Color.Black
        Me.labelCreditoDisponible.Location = New System.Drawing.Point(169, 58)
        Me.labelCreditoDisponible.Name = "labelCreditoDisponible"
        Me.labelCreditoDisponible.Size = New System.Drawing.Size(155, 27)
        Me.labelCreditoDisponible.TabIndex = 23
        Me.labelCreditoDisponible.Text = "0.00"
        Me.labelCreditoDisponible.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(4, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(170, 15)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "CREDITO SUPERVENTAS"
        '
        'labelCreditoLimite
        '
        Me.labelCreditoLimite.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelCreditoLimite.ForeColor = System.Drawing.Color.Blue
        Me.labelCreditoLimite.Location = New System.Drawing.Point(165, 13)
        Me.labelCreditoLimite.Name = "labelCreditoLimite"
        Me.labelCreditoLimite.Size = New System.Drawing.Size(159, 27)
        Me.labelCreditoLimite.TabIndex = 12
        Me.labelCreditoLimite.Text = "0.00"
        Me.labelCreditoLimite.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'labelCreditoLimite_
        '
        Me.labelCreditoLimite_.AutoSize = True
        Me.labelCreditoLimite_.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelCreditoLimite_.ForeColor = System.Drawing.Color.Black
        Me.labelCreditoLimite_.Location = New System.Drawing.Point(93, 17)
        Me.labelCreditoLimite_.Name = "labelCreditoLimite_"
        Me.labelCreditoLimite_.Size = New System.Drawing.Size(51, 18)
        Me.labelCreditoLimite_.TabIndex = 11
        Me.labelCreditoLimite_.Text = "Limite:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(36, 39)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(108, 18)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "Credito Usado:"
        '
        'btnTerminar
        '
        Me.btnTerminar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTerminar.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnTerminar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTerminar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnTerminar.Location = New System.Drawing.Point(11, 405)
        Me.btnTerminar.Name = "btnTerminar"
        Me.btnTerminar.Size = New System.Drawing.Size(105, 43)
        Me.btnTerminar.TabIndex = 0
        Me.btnTerminar.Text = "Terminar"
        Me.btnTerminar.UseVisualStyleBackColor = True
        '
        'btnTerminarEImprimir
        '
        Me.btnTerminarEImprimir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTerminarEImprimir.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.btnTerminarEImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTerminarEImprimir.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnTerminarEImprimir.Location = New System.Drawing.Point(122, 405)
        Me.btnTerminarEImprimir.Name = "btnTerminarEImprimir"
        Me.btnTerminarEImprimir.Size = New System.Drawing.Size(105, 43)
        Me.btnTerminarEImprimir.TabIndex = 1
        Me.btnTerminarEImprimir.Text = "Imprimir y Terminar"
        Me.btnTerminarEImprimir.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.ForeColor = System.Drawing.Color.Red
        Me.btnCancelar.Location = New System.Drawing.Point(274, 405)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(101, 43)
        Me.btnCancelar.TabIndex = 2
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'tlpImportes
        '
        Me.tlpImportes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpImportes.ColumnCount = 4
        Me.tlpImportes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.tlpImportes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 122.0!))
        Me.tlpImportes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.943089!))
        Me.tlpImportes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 91.05691!))
        Me.tlpImportes.Controls.Add(Me.Panel3, 2, 5)
        Me.tlpImportes.Controls.Add(Me.labelImporteVenta_Saldo, 0, 0)
        Me.tlpImportes.Controls.Add(Me.boxImporteVenta, 2, 0)
        Me.tlpImportes.Controls.Add(Me.labelCobrandoME, 1, 3)
        Me.tlpImportes.Controls.Add(Me.labelCobrandoCS, 1, 4)
        Me.tlpImportes.Controls.Add(Me.txtCobrandoCredito, 3, 4)
        Me.tlpImportes.Controls.Add(Me.labelImportePagoCliente_TOTAL, 1, 5)
        Me.tlpImportes.Controls.Add(Me.labelCobrandoEfectivoRecibido, 1, 2)
        Me.tlpImportes.Controls.Add(Me.labelAdeudoAPagar, 1, 6)
        Me.tlpImportes.Controls.Add(Me.labelVuelto_SaldoNuevo, 0, 8)
        Me.tlpImportes.Controls.Add(Me.boxVuelto, 2, 8)
        Me.tlpImportes.Controls.Add(Me.boxImportePagoCliente_TOTAL, 3, 5)
        Me.tlpImportes.Controls.Add(Me.btnPendientes, 2, 4)
        Me.tlpImportes.Controls.Add(Me.Panel1, 0, 5)
        Me.tlpImportes.Controls.Add(Me.txtCobrandoME, 3, 3)
        Me.tlpImportes.Controls.Add(Me.txtAdeudoAPagar, 3, 6)
        Me.tlpImportes.Controls.Add(Me.txtCobrandoEfectivoRecibido, 3, 2)
        Me.tlpImportes.Controls.Add(Me.labelEfectivoRecibido, 3, 1)
        Me.tlpImportes.Location = New System.Drawing.Point(3, 3)
        Me.tlpImportes.Name = "tlpImportes"
        Me.tlpImportes.RowCount = 9
        Me.tlpImportes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.14165!))
        Me.tlpImportes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpImportes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.81533!))
        Me.tlpImportes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.81533!))
        Me.tlpImportes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.16376!))
        Me.tlpImportes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.69338!))
        Me.tlpImportes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpImportes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.545455!))
        Me.tlpImportes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlpImportes.Size = New System.Drawing.Size(375, 387)
        Me.tlpImportes.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(148, 222)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(20, 68)
        Me.Panel3.TabIndex = 23
        '
        'labelImporteVenta_Saldo
        '
        Me.labelImporteVenta_Saldo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labelImporteVenta_Saldo.BackColor = System.Drawing.Color.Black
        Me.tlpImportes.SetColumnSpan(Me.labelImporteVenta_Saldo, 2)
        Me.labelImporteVenta_Saldo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelImporteVenta_Saldo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.labelImporteVenta_Saldo.Location = New System.Drawing.Point(3, 3)
        Me.labelImporteVenta_Saldo.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.labelImporteVenta_Saldo.Name = "labelImporteVenta_Saldo"
        Me.labelImporteVenta_Saldo.Size = New System.Drawing.Size(145, 37)
        Me.labelImporteVenta_Saldo.TabIndex = 14
        Me.labelImporteVenta_Saldo.Text = "Importe de " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "la Venta"
        Me.labelImporteVenta_Saldo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'boxImporteVenta
        '
        Me.boxImporteVenta.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.boxImporteVenta.AutoSize = True
        Me.boxImporteVenta.BackColor = System.Drawing.Color.Black
        Me.tlpImportes.SetColumnSpan(Me.boxImporteVenta, 2)
        Me.boxImporteVenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.boxImporteVenta.ForeColor = System.Drawing.Color.White
        Me.boxImporteVenta.Location = New System.Drawing.Point(151, 3)
        Me.boxImporteVenta.Margin = New System.Windows.Forms.Padding(3)
        Me.boxImporteVenta.Name = "boxImporteVenta"
        Me.boxImporteVenta.Size = New System.Drawing.Size(221, 37)
        Me.boxImporteVenta.TabIndex = 0
        Me.boxImporteVenta.Text = "0.00"
        Me.boxImporteVenta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'labelCobrandoME
        '
        Me.labelCobrandoME.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labelCobrandoME.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelCobrandoME.ForeColor = System.Drawing.Color.Blue
        Me.labelCobrandoME.Location = New System.Drawing.Point(29, 113)
        Me.labelCobrandoME.Name = "labelCobrandoME"
        Me.labelCobrandoME.Size = New System.Drawing.Size(116, 36)
        Me.labelCobrandoME.TabIndex = 17
        Me.labelCobrandoME.Text = "Monedero Electronico"
        Me.labelCobrandoME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labelCobrandoCS
        '
        Me.labelCobrandoCS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labelCobrandoCS.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelCobrandoCS.ForeColor = System.Drawing.Color.Blue
        Me.labelCobrandoCS.Location = New System.Drawing.Point(29, 167)
        Me.labelCobrandoCS.Name = "labelCobrandoCS"
        Me.labelCobrandoCS.Size = New System.Drawing.Size(116, 36)
        Me.labelCobrandoCS.TabIndex = 18
        Me.labelCobrandoCS.Text = "Credito SuperVenta"
        Me.labelCobrandoCS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCobrandoCredito
        '
        Me.txtCobrandoCredito.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCobrandoCredito.Location = New System.Drawing.Point(171, 170)
        Me.txtCobrandoCredito.MaxLength = 15
        Me.txtCobrandoCredito.Name = "txtCobrandoCredito"
        Me.txtCobrandoCredito.ShortcutsEnabled = False
        Me.txtCobrandoCredito.Size = New System.Drawing.Size(201, 44)
        Me.txtCobrandoCredito.TabIndex = 2
        Me.txtCobrandoCredito.Text = "0.00"
        Me.txtCobrandoCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'labelImportePagoCliente_TOTAL
        '
        Me.labelImportePagoCliente_TOTAL.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.labelImportePagoCliente_TOTAL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.labelImportePagoCliente_TOTAL.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelImportePagoCliente_TOTAL.ForeColor = System.Drawing.Color.Maroon
        Me.labelImportePagoCliente_TOTAL.Location = New System.Drawing.Point(26, 222)
        Me.labelImportePagoCliente_TOTAL.Margin = New System.Windows.Forms.Padding(0)
        Me.labelImportePagoCliente_TOTAL.Name = "labelImportePagoCliente_TOTAL"
        Me.labelImportePagoCliente_TOTAL.Size = New System.Drawing.Size(122, 68)
        Me.labelImportePagoCliente_TOTAL.TabIndex = 20
        Me.labelImportePagoCliente_TOTAL.Text = "TOTAL"
        Me.labelImportePagoCliente_TOTAL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labelCobrandoEfectivoRecibido
        '
        Me.labelCobrandoEfectivoRecibido.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labelCobrandoEfectivoRecibido.AutoSize = True
        Me.labelCobrandoEfectivoRecibido.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelCobrandoEfectivoRecibido.ForeColor = System.Drawing.Color.Blue
        Me.labelCobrandoEfectivoRecibido.Location = New System.Drawing.Point(29, 59)
        Me.labelCobrandoEfectivoRecibido.Name = "labelCobrandoEfectivoRecibido"
        Me.labelCobrandoEfectivoRecibido.Size = New System.Drawing.Size(116, 36)
        Me.labelCobrandoEfectivoRecibido.TabIndex = 16
        Me.labelCobrandoEfectivoRecibido.Text = "Efectivo Recibido"
        Me.labelCobrandoEfectivoRecibido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labelAdeudoAPagar
        '
        Me.labelAdeudoAPagar.BackColor = System.Drawing.Color.Red
        Me.labelAdeudoAPagar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.labelAdeudoAPagar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelAdeudoAPagar.ForeColor = System.Drawing.Color.Yellow
        Me.labelAdeudoAPagar.Location = New System.Drawing.Point(26, 290)
        Me.labelAdeudoAPagar.Margin = New System.Windows.Forms.Padding(0)
        Me.labelAdeudoAPagar.Name = "labelAdeudoAPagar"
        Me.labelAdeudoAPagar.Size = New System.Drawing.Size(122, 42)
        Me.labelAdeudoAPagar.TabIndex = 21
        Me.labelAdeudoAPagar.Text = "Pago Adeudo"
        Me.labelAdeudoAPagar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labelVuelto_SaldoNuevo
        '
        Me.labelVuelto_SaldoNuevo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpImportes.SetColumnSpan(Me.labelVuelto_SaldoNuevo, 2)
        Me.labelVuelto_SaldoNuevo.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelVuelto_SaldoNuevo.Location = New System.Drawing.Point(3, 345)
        Me.labelVuelto_SaldoNuevo.Name = "labelVuelto_SaldoNuevo"
        Me.labelVuelto_SaldoNuevo.Size = New System.Drawing.Size(142, 42)
        Me.labelVuelto_SaldoNuevo.TabIndex = 15
        Me.labelVuelto_SaldoNuevo.Text = "Vuelto"
        Me.labelVuelto_SaldoNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'boxVuelto
        '
        Me.boxVuelto.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.boxVuelto.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.tlpImportes.SetColumnSpan(Me.boxVuelto, 2)
        Me.boxVuelto.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.boxVuelto.ForeColor = System.Drawing.Color.Black
        Me.boxVuelto.Location = New System.Drawing.Point(151, 345)
        Me.boxVuelto.Name = "boxVuelto"
        Me.boxVuelto.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.boxVuelto.Size = New System.Drawing.Size(221, 42)
        Me.boxVuelto.TabIndex = 4
        Me.boxVuelto.Text = "0.00"
        Me.boxVuelto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'boxImportePagoCliente_TOTAL
        '
        Me.boxImportePagoCliente_TOTAL.AutoSize = True
        Me.boxImportePagoCliente_TOTAL.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.boxImportePagoCliente_TOTAL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.boxImportePagoCliente_TOTAL.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.boxImportePagoCliente_TOTAL.Location = New System.Drawing.Point(168, 222)
        Me.boxImportePagoCliente_TOTAL.Margin = New System.Windows.Forms.Padding(0)
        Me.boxImportePagoCliente_TOTAL.Name = "boxImportePagoCliente_TOTAL"
        Me.boxImportePagoCliente_TOTAL.Size = New System.Drawing.Size(207, 68)
        Me.boxImportePagoCliente_TOTAL.TabIndex = 3
        Me.boxImportePagoCliente_TOTAL.Text = "0.00"
        Me.boxImportePagoCliente_TOTAL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnPendientes
        '
        Me.btnPendientes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPendientes.Location = New System.Drawing.Point(150, 169)
        Me.btnPendientes.Margin = New System.Windows.Forms.Padding(2)
        Me.btnPendientes.Name = "btnPendientes"
        Me.btnPendientes.Size = New System.Drawing.Size(16, 42)
        Me.btnPendientes.TabIndex = 3
        Me.btnPendientes.Text = "P"
        Me.btnPendientes.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 222)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(26, 68)
        Me.Panel1.TabIndex = 22
        '
        'txtCobrandoME
        '
        Me.txtCobrandoME.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCobrandoME.Location = New System.Drawing.Point(171, 116)
        Me.txtCobrandoME.MaxLength = 15
        Me.txtCobrandoME.Name = "txtCobrandoME"
        Me.txtCobrandoME.ShortcutsEnabled = False
        Me.txtCobrandoME.Size = New System.Drawing.Size(201, 44)
        Me.txtCobrandoME.TabIndex = 1
        Me.txtCobrandoME.Text = "0.00"
        Me.txtCobrandoME.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAdeudoAPagar
        '
        Me.txtAdeudoAPagar.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAdeudoAPagar.BackColor = System.Drawing.Color.Red
        Me.txtAdeudoAPagar.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAdeudoAPagar.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdeudoAPagar.ForeColor = System.Drawing.Color.Yellow
        Me.txtAdeudoAPagar.Location = New System.Drawing.Point(168, 290)
        Me.txtAdeudoAPagar.Margin = New System.Windows.Forms.Padding(0)
        Me.txtAdeudoAPagar.MaxLength = 15
        Me.txtAdeudoAPagar.Name = "txtAdeudoAPagar"
        Me.txtAdeudoAPagar.ShortcutsEnabled = False
        Me.txtAdeudoAPagar.Size = New System.Drawing.Size(207, 40)
        Me.txtAdeudoAPagar.TabIndex = 4
        Me.txtAdeudoAPagar.Text = "0.00"
        Me.txtAdeudoAPagar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCobrandoEfectivoRecibido
        '
        Me.txtCobrandoEfectivoRecibido.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCobrandoEfectivoRecibido.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCobrandoEfectivoRecibido.Location = New System.Drawing.Point(171, 62)
        Me.txtCobrandoEfectivoRecibido.Name = "txtCobrandoEfectivoRecibido"
        Me.txtCobrandoEfectivoRecibido.Size = New System.Drawing.Size(201, 44)
        Me.txtCobrandoEfectivoRecibido.TabIndex = 0
        Me.txtCobrandoEfectivoRecibido.Text = "0.00"
        Me.txtCobrandoEfectivoRecibido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'labelEfectivoRecibido
        '
        Me.labelEfectivoRecibido.AutoSize = True
        Me.labelEfectivoRecibido.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelEfectivoRecibido.ForeColor = System.Drawing.Color.Red
        Me.labelEfectivoRecibido.Location = New System.Drawing.Point(171, 43)
        Me.labelEfectivoRecibido.Name = "labelEfectivoRecibido"
        Me.labelEfectivoRecibido.Size = New System.Drawing.Size(160, 16)
        Me.labelEfectivoRecibido.TabIndex = 21
        Me.labelEfectivoRecibido.Text = "Efectivo ya Capturado"
        Me.labelEfectivoRecibido.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'panelCapturaImportes
        '
        Me.panelCapturaImportes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panelCapturaImportes.Controls.Add(Me.tlpImportes)
        Me.panelCapturaImportes.Controls.Add(Me.btnTerminar)
        Me.panelCapturaImportes.Controls.Add(Me.btnCancelar)
        Me.panelCapturaImportes.Controls.Add(Me.btnTerminarEImprimir)
        Me.panelCapturaImportes.Location = New System.Drawing.Point(3, 3)
        Me.panelCapturaImportes.Name = "panelCapturaImportes"
        Me.panelCapturaImportes.Size = New System.Drawing.Size(381, 462)
        Me.panelCapturaImportes.TabIndex = 0
        '
        'panelCaptura
        '
        Me.panelCaptura.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panelCaptura.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panelCaptura.Controls.Add(Me.panelCapturaImportes)
        Me.panelCaptura.Location = New System.Drawing.Point(429, 12)
        Me.panelCaptura.Name = "panelCaptura"
        Me.panelCaptura.Size = New System.Drawing.Size(391, 475)
        Me.panelCaptura.TabIndex = 1
        '
        'timerParpadeaAdeudo
        '
        Me.timerParpadeaAdeudo.Interval = 300
        '
        'timerParpadeaLables
        '
        Me.timerParpadeaLables.Interval = 300
        '
        'Cobrando
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Controls.Add(Me.panelCaptura)
        Me.Controls.Add(Me.panelDatos)
        Me.Name = "Cobrando"
        Me.Size = New System.Drawing.Size(832, 501)
        Me.panelDatos.ResumeLayout(False)
        Me.tlpDatos.ResumeLayout(False)
        CType(Me.picClienteFoto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelDatosCliente.ResumeLayout(False)
        Me.panelDatosCliente.PerformLayout()
        Me.panelMonederoElectronico.ResumeLayout(False)
        Me.panelMonederoElectronico.PerformLayout()
        Me.panelCreditoSuperventas.ResumeLayout(False)
        Me.panelCreditoSuperventas.PerformLayout()
        Me.tlpImportes.ResumeLayout(False)
        Me.tlpImportes.PerformLayout()
        Me.panelCapturaImportes.ResumeLayout(False)
        Me.panelCaptura.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelDatos As System.Windows.Forms.Panel
    Friend WithEvents btnTerminar As System.Windows.Forms.Button
    Friend WithEvents btnTerminarEImprimir As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents label_ME_Disponible As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents label_ME_ImporteEstaVenta As System.Windows.Forms.Label
    Friend WithEvents label_ME_label As System.Windows.Forms.Label
    Friend WithEvents labelClienteNombre As System.Windows.Forms.Label
    Friend WithEvents picClienteFoto As System.Windows.Forms.PictureBox
    Friend WithEvents labelCreditoLimite As System.Windows.Forms.Label
    Friend WithEvents labelCreditoLimite_ As System.Windows.Forms.Label
    Friend WithEvents labelCreditoUsado As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents tlpDatos As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents panelDatosCliente As System.Windows.Forms.Panel
    Friend WithEvents panelMonederoElectronico As System.Windows.Forms.Panel
    Friend WithEvents panelCreditoSuperventas As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents labelClienteClave As System.Windows.Forms.Label
    Friend WithEvents tlpImportes As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents labelImporteVenta_Saldo As System.Windows.Forms.Label
    Friend WithEvents boxImporteVenta As System.Windows.Forms.Label
    Friend WithEvents labelCobrandoME As System.Windows.Forms.Label
    Friend WithEvents labelCobrandoCS As System.Windows.Forms.Label
    Friend WithEvents labelImportePagoCliente_TOTAL As System.Windows.Forms.Label
    Friend WithEvents labelVuelto_SaldoNuevo As System.Windows.Forms.Label
    Friend WithEvents txtCobrandoEfectivoRecibido As System.Windows.Forms.TextBox
    Friend WithEvents txtCobrandoCredito As System.Windows.Forms.TextBox
    Friend WithEvents boxVuelto As System.Windows.Forms.Label
    Friend WithEvents labelCobrandoEfectivoRecibido As System.Windows.Forms.Label
    Friend WithEvents panelCapturaImportes As System.Windows.Forms.Panel
    Friend WithEvents panelCaptura As System.Windows.Forms.Panel
    Friend WithEvents labelCreditoDisponible As System.Windows.Forms.Label
    Friend WithEvents labelCreditoDisponible_ As System.Windows.Forms.Label
    Friend WithEvents labelEfectivoRecibido As System.Windows.Forms.Label
    Friend WithEvents labelAdeudoAPagar As System.Windows.Forms.Label
    Friend WithEvents timerParpadeaAdeudo As System.Windows.Forms.Timer
    Friend WithEvents btnPendientes As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txtCobrandoME As System.Windows.Forms.TextBox
    Friend WithEvents timerParpadeaLables As System.Windows.Forms.Timer
    Friend WithEvents boxImportePagoCliente_TOTAL As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtAdeudoAPagar As System.Windows.Forms.TextBox

End Class
