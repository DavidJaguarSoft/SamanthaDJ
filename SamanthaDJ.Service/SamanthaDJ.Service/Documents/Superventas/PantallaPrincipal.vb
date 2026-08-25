'Programa Super Ventas Control de Inventarios
'
'                      Programador: David Jaguar Soft
'                       Plataforma: Visual Basic Net 2010, SQL Server 2008 y Crystal Report
'                  Fecha de Inicio: Enero de 2010
'______________________________________________________________________________________________________

#Region "Imports"
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Mensajes
'Imports Cobrando
#End Region

Public Class PantallaPrincipal
    '
#Region "Variables"
    Dim panloc As New Point(0, 0)
    Dim curloc As New Point(0, 0)
    '
    Private vf_txt_LeerCodigo_Aux As String = ""
    Private vf_PosicionAsterisco As Integer = 0
    Private vf_Total_Costo_MovAlmacen As Decimal = 0
    Private vf_GranTOTAL As Decimal = 0
    Private vf_ClienteYaSeleccionado As Boolean = False

    Private vf_ContadorEnter As Integer = 0
    Private vf_ContadorItems As Integer = 0

    Private vf_Ventas_FolioAGrabar As Double = 0
    Private vf_VentaCancelada_Justificacion As String = ""
    '
    Private _Producto_Cantidad As Decimal = 1
    Private _Producto_CodigoBarras As String = ""
    Private _Producto_Id As String = ""
    '
    Private vf_MovAlmacen_FolioAGrabar As Double = 0
    '
    'Datos del Producto
    Private _Producto_Descripcion As String = ""
    Private _Producto_DescripcionAdicional As String = ""
    Private _Producto_PrecioCosto As Decimal = 0
    Private _Producto_PrecioVentaMenudeo As Decimal = 0
    Private _Producto_PrecioVendido As Decimal = 0
    '
    Private _Producto_MedioMayoreo_Aplica As Boolean
    Private _Producto_MedioMayoreo_Precio As Decimal = 0
    Private _Producto_MedioMayoreo_CantidadMinima As Decimal = 0
    Private _Producto_Mayoreo_Aplica As Boolean
    Private _Producto_Mayoreo_Precio As Decimal = 0
    Private _Producto_Mayoreo_CantidadMinima As Decimal = 0
    Private _Producto_GranMayoreo_Aplica As Boolean
    Private _Producto_GranMayoreo_Precio As Decimal = 0
    Private _Producto_GranMayoreo_CantidadMinima As Decimal = 0
    '
    Private _Producto_Embalaje As String = ""
    Private _Producto_CantidadDisponible As Decimal = 0
    Private _Producto_Foto As String = ""
    Private _Producto_RequiereDescripcion As Boolean = False
    Private _Producto_NoPuedeSePuedeVenderSinoDisponible As Boolean = False
    '
    Private _Producto_DesconEn As Decimal
    Private _Producto_Descon_1_Aplica As Boolean
    Private _Producto_Descon_1_DesconsolidarEn As Decimal
    Private _Producto_Descon_1_IdEmbalaje As Byte
    Private _Producto_Descon_1_Descripcion As String
    Private _Producto_Descon_1_PrecioCosto As Decimal
    Private _Producto_Descon_1_Utilidad As Decimal
    Private _Producto_Descon_1_PrecioVenta As Decimal
    Private _Producto_Descon_1_AplicaEsquemaPreciosMayoreo As Boolean
    Private _Producto_Descon_2_Aplica As Boolean
    Private _Producto_Descon_2_DesconsolidarEn As Decimal
    Private _Producto_Descon_2_IdEmbalaje As Byte
    Private _Producto_Descon_2_Descripcion As String
    Private _Producto_Descon_2_PrecioCosto As Decimal
    Private _Producto_Descon_2_Utilidad As Decimal
    Private _Producto_Descon_2_PrecioVenta As Decimal
    Private _Producto_Descon_2_AplicaEsquemaPreciosMayoreo As Boolean
    Private _Producto_Descon_3_Aplica As Boolean
    Private _Producto_Descon_3_DesconsolidarEn As Decimal
    Private _Producto_Descon_3_IdEmbalaje As Byte
    Private _Producto_Descon_3_Descripcion As String
    Private _Producto_Descon_3_PrecioCosto As Decimal
    Private _Producto_Descon_3_Utilidad As Decimal
    Private _Producto_Descon_3_PrecioVenta As Decimal
    Private _Producto_Descon_3_AplicaEsquemaPreciosMayoreo As Boolean
    Private _Producto_ME_Aplica As Boolean
    Private _Producto_ME_MenudeoAplicaSuPropioFactor As Boolean
    Private _Producto_ME_MenudeoFactor As Decimal
    Private _Producto_ME_MedioMayAplicaSuPropioFactor As Boolean
    Private _Producto_ME_MedioMayFactor As Decimal
    Private _Producto_ME_MayoreoAplicaSuPropioFactor As Boolean
    Private _Producto_ME_MayoreoFactor As Decimal
    Private _Producto_ME_GranMayAplicaSuPropioFactor As Boolean
    Private _Producto_ME_GranMayFactor As Decimal
    '
    'Datos del Cliente
    Private _Cliente_Id As Integer = 1
    Private _Cliente_RazonSocial As String = ""
    Private _Cliente_Precios_ObligatorioTarjeta As Boolean = False
    Private _Cliente_Precios_RangoAplicado As String = ""
    Private _Cliente_Precios_PorcentADefinir As Decimal = 0
    Private _Cliente_Precios_PreguntarSiImprimirMayMen As Boolean = False
    Private _Cliente_ME_Aplica As Boolean
    Private _Cliente_ME_FactorPrincipal As Decimal
    Private _Cliente_ME_SiImporteMEExcedeUtilidadEn As Decimal
    Private _Cliente_ME_AplicaEsteFactorAlterno As Decimal
    Private _Cliente_ME_Disponible As Decimal = 0
    Private _Cliente_ME_MinimoCambiar
    Private _Cliente_Credito_Aplica As Boolean
    Private _Cliente_Credito_Deshabilitado As Boolean
    Private _Cliente_Credito_Dias As Integer 'Dias para pagar la "Cuenta x pagar" que genera la compra del cliente
    Private _Cliente_Credito_InteresMensual As Decimal
    Private _Cliente_Credito_Limite As Decimal
    Private _Cliente_Credito_Usado As Decimal
    Private _Cliente_Credito_Disponible As Decimal = 0
    Private _Cliente_Credito_BloquearSiDebe As Boolean = True
    Private _Cliente_Credito_MinimoCompra As Decimal
    Private _Cliente_FotoCliente As String = ""
    Private _Cliente_FotoMasivaCliente As String = ""
    Private _Cliente_InternoAplica As Boolean = False
    Private _Cliente_InternoCuentaContableCargo As Integer = 0
    Private _Cliente_InternoCuentaContableAbono As Integer = 0
    Private _Cliente_Bloqueado As Boolean
    '
    'Dataos de la TARJETA
    Private _Tarjeta_Numero As String = ""
    Private _Tarjeta_Id As Integer = 0
    Private _Tarjeta_Existe As Boolean = False
    Private _Tarjeta_ClienteId As Integer = 0
    Private _Tarjeta_ME_Activada As Boolean = False
    Private _Tarjeta_ME_FactorPrincipal As Decimal = 0
    Private _Tarjeta_ME_SiImporteMEExcedeUtilidadEn As Decimal = 0
    Private _Tarjeta_ME_AplicaEsteFactorAlterno As Decimal = 0
    Private _Tarjeta_ME_Disponible As Decimal = 0
    Private _Tarjeta_CreditoActivada As Boolean = False
    Private _Tarjeta_CreditoLimite As Decimal = 0
    Private _Tarjeta_CreditoUsado As Decimal = 0
    Private _Tarjeta_CreditoDisponible As Decimal = 0
    Private _Tarjeta_Cancelada As Boolean = False
    '
    'Cobranza
    Private _ImportePositivo_Venta As Decimal = 0
    Private _ImporteNegativo_Venta As Decimal = 0
    Private _ImportePago_Efectivo As Decimal = 0
    Private _ImportePago_ME As Decimal = 0
    Private _ImportePago_Credito As Decimal = 0
    'Private _ImportePago_OtroMedio As Decimal = 0
    Private _ImportePago_Referencia As String = ""
    Private _ImportePago_TOTAL As Decimal = 0
    Private _Importe_Vuelto As Decimal = 0
    Private _Importe_AdeudoAPagar As Decimal = 0
    Private _ImporteYaSeCapturoElPagoDelCliente As Boolean = False
    '
    'Indices del DataGridView
    Private IdxCodigoBarras As Integer = 3
    Private IdxDescripcionProducto As Integer = 4
    Private IdxFoto As Integer = 30
    Private IdxFotoMasivo As Integer = 34
    '
    'Control de Imagen de la FotoProducto
    Private ProductoTieneImagen As Boolean = False
    '
    'Variable Auxiliar para controlar el tamaño de las etiquetas o labels que parpadean, aumentando y disminuyendo su tamaño
    Private _LetraTamano As Decimal = 9
    Private _LetraAumentando = True
#End Region
    '
#Region "Variables de Clase"
    'Controles
    Private _NumControlesEnPanelTrabajoDerecho As Integer
    '
    Private cTarjeta As Tarjetas = New Tarjetas
    Private _ctrPanelControl As PanelControl = New PanelControl()
    Private _ctrAdministradorDeTarjetas As AdministradorDeTarjetas = New AdministradorDeTarjetas()
    Private _ctrCatalogos As Catalogos = New Catalogos()
    Private _ctrFlujoEfectivo As FlujoDeEfectivo = New FlujoDeEfectivo()
    Private _ctrCreditos As ctrCreditos = New ctrCreditos()
    Private _ctrAcercaDe As AcercaDe = New AcercaDe()
    Private _ctrCobrando As Cobrando = New Cobrando(0, "", Nothing, False, 0, 0, False, False, 0, False, 0, 0, 0, 0, 0, "", 0, 0, 0, 0, False, False, False, "", "", False, 0, False, 0, False, 0, 0, 0, False, False)
    Private cCatProd As ctrCatalogoProductos = New ctrCatalogoProductos("")
    Private cAdministrarProductos As ctrAdministrarProductos = New ctrAdministrarProductos()
    Private cImpresionPrecios As ctrImprimirPrecios = New ctrImprimirPrecios()
    Private cCatalogoClientes As CatalogoClientes = New CatalogoClientes()
    Private cClientePendientes As ClientePendientes = New ClientePendientes()
    Private cHistorialCrediticio As HistorialCrediticio = New HistorialCrediticio(0, "")
#End Region
    '
#Region "PantallaPrincipal"
    Private Sub fPantallaPrincipal_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 27 Then
            RemoverControles_PanelTrabajoDerecho()
        End If
        If e.KeyCode = 111 Then    'Oprimio la tecla "/"
            If dgv_Ventas.Rows.Count > 0 Then
                PanelNegociacion_Llamar()
            End If
        End If
    End Sub
    Private Sub PantallaPrincipal_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress

    End Sub
    Private Sub PantallaPrincipal_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.panel_demo.Location = New Point((Me.panelTrabajoDerecho.Size.Width - 350) / 2, (Me.panelTrabajoDerecho.Size.Height - 110) / 2)
        'Me.SplitContainer_Contenido.Location = New Point((Me.SplitContainer_Contenido.Panel2.Size.Width - 944) / 2, (Me.SplitContainer_Contenido.Panel2.Size.Height - 511) / 2)
    End Sub
    Private Sub fPantallaPrincipal_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        txtLeerCodigo.Focus()
    End Sub
    Private Sub PantallaPrincipal_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim linGrBrush As New LinearGradientBrush(New Point(0, 0), New Point(Me.Width, Me.Height), Color.Yellow, Color.White)
        ''Dim pen As New Pen(linGrBrush)

        ''e.Graphics.DrawLine(pen, 0, 10, 200, 10)
        ''e.Graphics.FillEllipse(linGrBrush, 0, 30, 200, 100)
        'e.Graphics.FillRectangle(linGrBrush, 0, 0, Me.Width, Me.Height)
    End Sub
    Private Sub PantallaPrincipal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '
        VerificaElEntorno()
        '
        AddHandler _ctrPanelControl.btnFlujoEfectivo_AbrirEvent, AddressOf PanelControl_EventFlujoEfectivoAbrir
        AddHandler _ctrPanelControl.btnCreditos_AbrirEvent, AddressOf PanelControl_EventCreditosAbrir
        AddHandler _ctrPanelControl.btnCatalogos_AbrirEvent, AddressOf PanelControl_EventCatalogosAbrir
        AddHandler _ctrPanelControl.btnTarjetas_AbrirEvent, AddressOf PanelControl_EventTarjetasAbrir
        '
        AddHandler _ctrFlujoEfectivo.btnCerrar_ClickEvent, AddressOf FlujoEfectivo_EventCerrar
        AddHandler _ctrCobrando.ClickBotonesTerminar, AddressOf Cobrando_EventCerrar
        AddHandler _ctrAcercaDe.btnCerrar_ClickEvent, AddressOf AcercaDe_EventCerrar
        AddHandler _ctrPanelControl.btnAdministrarProductos_AbrirEvent, AddressOf AdministrarProductos
        'AddHandler cAdministrarProductos.btnCatalogoProductos_ClickEvent, AddressOf AbrirCatalogoProductos
        'AddHandler cAdministrarProductos.btnImpresionPrecios_ClickEvent, AddressOf AbrirImpresionPrecios
        AddHandler cImpresionPrecios.btnCerrar_ClickEvent, AddressOf CerrarImpresionPrecios
        '
        Me.panel_demo.Location = New Point((Me.panelTrabajoDerecho.Size.Width - 350) / 2, (Me.panelTrabajoDerecho.Size.Height - 110) / 2)
        lnk_EsUnDemo_1.Text = "Este Programa es una versión Gratuita de Evaluación y se desactivará en " & Str(gEntorno_DiasQueQuedanDeLaEvaluacion) & " días."
        lnk_EsUnDemo_2.Text = "Por favor Registre su Sistema y disfrute de sus beneficios tales como Actualizaciones y Asesoría Técnica Gratis."
        Timer_ReubicarPanelDemo.Interval = 1
        If Trim(gConfigSistema_Llave) = "" Or Trim(gConfigSistema_ContraLlave) = "" Then
            If gEntorno_VerLeyendaEsUnDemo Then
                panel_demo.Visible = True
            Else
                panel_demo.Visible = False
            End If
        Else
            panel_demo.Visible = False
        End If
        PanelDemo_SetPositions()
        '
        StatusStripPrincipal_labelVersion.Text = "Super Ventas v" & gEntorno_Version
        StatusStripPrincipal_labelLeAtiende.Text = "   Le Atiende: " & Format(gUsuario_Id, "000") & " " & Trim(gUsuario_Nombre) & "   "
        StatusStripPrincipal_Mensajes.Text = "Buenos dias !"
        '
        Try    'Si por algun motivo no encuentra el archivo de imagen, no se detiene el sistema
            picLogo.BackgroundImage = cLibreriasImagen.StringToImage(gPersonalizacion("Gen_LogoNombreArchivo").ToString())
        Catch
        End Try
        '
        Me.Text = "Super Ventas"
        '
        If Trim(Per_SucursalNombre) = "" Then
            lnk_NombreComercial.Text = Per_NombreComercial
        Else
            lnk_NombreComercial.Text = Per_NombreComercial & vbCrLf & "Suc. " & Per_SucursalNombre
        End If
        '
        'btnAgenda.Text = FormatDateTime(vg_ConfigSistema_FechaActual, DateFormat.LongDate) & "  " & FormatDateTime(TimeOfDay, DateFormat.ShortTime)
        '
        If FechaSinHora(gUsuario_FechaCaducaFirma) < FechaSinHora(gConfigSistema_FechaActual) Then
            Dim pivote As New form_CambioContrasena(gUsuario_Id, gUsuario_Nombre)
            pivote.ShowDialog()
        End If
        If vg_PF.fun_ExistenProductosQueCambiaronDePrecio > 0 Then
            form_MuestraPendientes.ShowDialog()
        End If
        Timer_ActualizaPantalla.Start()
        Timer_LicenciaDeUso.Start()
        Timer_ChecaPendientes.Start()
        '
        'Carga Controles de Punto de Venta
        prog_InicializaVariablesXProducto()
        prog_InicializaVariablesXVenta()
        cLibreriasBD.InsertUpdate("DELETE FROM VentasPV_Detalle WHERE ProductoId = 0 OR CantidadSurtida = 0 OR PUVenta = 0 OR PUVendido = 0")
        'prog_EliminaVaciosDeTemp()
        RecargaGridVentas()
        prog_MuestraOcultaBotones()
        txtLeerCodigo.Text = ""
        txtLeerCodigo.Focus()
        '
        Me.panelTrabajoIzquierdoInterno.Controls.Add(_ctrPanelControl)
        Me._ctrPanelControl.Dock = DockStyle.Fill
        ToolStripPanelAdministrador_Minimizar()
        '
        _NumControlesEnPanelTrabajoDerecho = Me.panelTrabajoDerecho.Controls.Count()
        'MessageBox.Show(_NumControlesEnPanelTrabajoDerecho.ToString())
        '
        Me.panelTrabajoIzquierdoInterno.Visible = True
        Me.ToolStripPanelAdministrador.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.ToolStripPanelAdministrador_botonActivar.TextImageRelation = TextImageRelation.TextBeforeImage
        Me.ToolStripPanelAdministrador_botonActivar.TextDirection = ToolStripTextDirection.Horizontal
        Me.SplitContainerTrabajo.SplitterDistance = 175
    End Sub 'form_PantallaPrincipal_Load
    Private Sub fPantallaPrincipal_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If gEntorno_Reiniciar Then
            '
        Else
            End
        End If
    End Sub
#End Region

#Region "PicLogo"
    Private Sub picLogo_Click(sender As Object, e As EventArgs) Handles picLogo.Click
        Try
            Dim pivote As New form_ImagenAmpliada(gPersonalizacion("Gen_LogoNombreArchivo").ToString(), "Logo Empresarial")
            If pivote.ShowDialog() = DialogResult.OK Then
                '
            End If
        Catch
        End Try
    End Sub
#End Region

#Region "btnListaRapida"
    Private Sub btnListaRapida_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListaRapida.GotFocus
        Me.txtLeerCodigo.Focus()
    End Sub
#End Region

#Region "txtLeerCodigo"
    Private Sub txtLeerCodigo_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLeerCodigo.Enter
        Me.txtLeerCodigo.BackColor = Color.Yellow
    End Sub
    Private Sub txtLeerCodigo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLeerCodigo.GotFocus
        Me.txtLeerCodigo.BackColor = Color.Yellow
    End Sub
    Private Sub txtLeerCodigo_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLeerCodigo.DoubleClick
        If vf_ContadorEnter = 0 Then
            My.Computer.Keyboard.SendKeys("{ENTER}", True)
            My.Computer.Keyboard.SendKeys("{ENTER}", True)
        Else
            My.Computer.Keyboard.SendKeys("{ENTER}", True)
        End If
    End Sub

    Private Sub txtLeerCodigo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLeerCodigo.KeyDown
        If e.KeyCode = 34 Then          'Se oprimio la tecla <PagDown> para Repetir el ultimo producto vendido
            prog_ObtenerCantidadCodigo(vf_txt_LeerCodigo_Aux, _Producto_Cantidad, _Producto_CodigoBarras)
            _Producto_Cantidad = 1
            txtLeerCodigo.Text = _Producto_CodigoBarras
            My.Computer.Keyboard.SendKeys("{ENTER}", True)
        End If
        '
        If e.KeyCode = 40 Then          'Cursor abajo
            My.Computer.Keyboard.SendKeys("{TAB}", True)
        End If
        '
        If e.KeyCode = 27 Then          'Se oprimio la tecla <ESC> 

        End If
    End Sub
    Private Sub txtLeerCodigo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLeerCodigo.KeyPress
        If InStr(1, Chr(13), e.KeyChar) > 0 Or InStr(1, "+", e.KeyChar) > 0 Then                            'if_9. El Usuario oprimio <Enter> o <+>
            txtLeerCodigo.Text = Trim(txtLeerCodigo.Text)
            vf_txt_LeerCodigo_Aux = txtLeerCodigo.Text
IniciaAnalisis:
            If Len(txtLeerCodigo.Text) > 0 Then                                                            'if_13. Si hay algun DATO capturado ?
                prog_ObtenerCantidadCodigo(txtLeerCodigo.Text, _Producto_Cantidad, _Producto_CodigoBarras)            'Desglosar Cantidad y Codigo
                If _Producto_Cantidad < 0 And Not gUsuario_PV_Productos_AceptarDevoluciones Then
                    cLibreriasMensaje.Mostrar("El Usuario no tiene permisos para aceptar devoluciones", Mensajes.Emergente.Advertencia)
                    Me.txtLeerCodigo.Focus()
                    GoTo Fin
                End If
                If Me.txtLeerCodigo.Text.Trim() = String.Empty Then
                    GoTo LlamaCatalogoProductos
                End If
                If Mid(txtLeerCodigo.Text, Len(txtLeerCodigo.Text), 1) = "*" Then
                    GoTo LlamaCatalogoProductos
                End If
ObtenerDatosDelProducto:
                If Mid(Me.txtLeerCodigo.Text, 1, 1) = "C" And Me.txtLeerCodigo.Text.Length() <= 4 And ClienteExiste(Mid(Me.txtLeerCodigo.Text, 2, 3)) Then
                    vf_ClienteYaSeleccionado = True
                    InicializaDatosTarjeta()
                    prog_ObtenerDatosClienteAVisualizar(Val(Mid(Me.txtLeerCodigo.Text, 2, 3)))
                    Me.txtLeerCodigo.Text = ""
                    Me.txtLeerCodigo.Clear()
                Else
                    If Mid(Trim(txtLeerCodigo.Text), 1, 7) = gPersonalizacion("TarjetaSV_PrefijoNumeroTarjeta").ToString() Then
                        vf_ClienteYaSeleccionado = True
                        ObtenerDatosClienteAPartirDeTarjeta()                                      'OBTENER DATOS DEL CLIENTE x TARJETA
                        txtLeerCodigo.Text = ""
                        txtLeerCodigo.Clear()
                    Else
                        If fun_DatosProductoAVisualizarMain(Val(_Producto_CodigoBarras)) Then                               'OBTENER DATOS DEL PRODUCTO
                            'Aqui ya podemos manipular la cantidad cuando se trata de kilos o gramos
                            If _Producto_Embalaje = 2 And _Producto_Cantidad > 20 Then
                                _Producto_Cantidad = _Producto_Cantidad / 1000
                            End If
                            '
                            If Per_Productos_SiExistenciaCero = "Segun perfil del Producto" Then
                                If _Producto_NoPuedeSePuedeVenderSinoDisponible Then
                                    If _Producto_Cantidad > _Producto_CantidadDisponible Then
                                        cLibreriasMensaje.Mostrar("El producto: " & _Producto_Descripcion & Chr(13) & _
                                                       "Solo tiene disponible: " & Str(_Producto_CantidadDisponible), Mensajes.Emergente.Error)
                                        GoTo Fin
                                    End If
                                End If
                            Else
                                If Per_Productos_SiExistenciaCero = "No Vender" Then
                                    If _Producto_Cantidad > _Producto_CantidadDisponible Then
                                        cLibreriasMensaje.Mostrar("El producto: " & _Producto_Descripcion & Chr(13) & _
                                                        "Solo tiene disponible: " & Str(_Producto_CantidadDisponible), Mensajes.Emergente.Error)
                                        GoTo Fin
                                    End If
                                End If
                            End If
                            '                                                                                              'DESCONSOLIDAR
                            If _Producto_Descon_1_Aplica Or _Producto_Descon_2_Aplica Or _Producto_Descon_3_Aplica Then
                                Dim piv_Desconsolidar As New form_PuntoDeVenta_Desconsolidacion(_Producto_CodigoBarras, _Producto_Descripcion, _
                                                                                                _Producto_Embalaje, _Producto_PrecioVentaMenudeo, _
                                                                                                _Producto_Descon_1_Aplica, _Producto_Descon_1_DesconsolidarEn, _
                                                                                                _Producto_Descon_1_IdEmbalaje, _Producto_Descon_1_Descripcion, _
                                                                                                _Producto_Descon_1_PrecioCosto, _Producto_Descon_1_PrecioVenta, _Producto_Descon_2_Aplica, _
                                                                                                _Producto_Descon_2_DesconsolidarEn, _Producto_Descon_2_IdEmbalaje, _
                                                                                                _Producto_Descon_2_Descripcion, _Producto_Descon_2_PrecioCosto, _Producto_Descon_2_PrecioVenta, _
                                                                                                _Producto_Descon_3_Aplica, _Producto_Descon_3_DesconsolidarEn, _
                                                                                                _Producto_Descon_3_IdEmbalaje, _Producto_Descon_3_Descripcion, _
                                                                                                _Producto_Descon_3_PrecioCosto, _Producto_Descon_3_PrecioVenta, _Producto_Cantidad)
                                If piv_Desconsolidar.ShowDialog = DialogResult.OK Then
                                    If piv_Desconsolidar.vDesconsolidacionSeleccionada Then
                                        _Producto_DesconEn = piv_Desconsolidar.vDesconEn
                                        _Producto_PrecioCosto = piv_Desconsolidar.vPrecioCosto
                                        _Producto_PrecioVentaMenudeo = piv_Desconsolidar.vPrecioVenta
                                        _Producto_PrecioVendido = piv_Desconsolidar.vPrecioVenta
                                        _Producto_DescripcionAdicional = "Desconsolidado en " & piv_Desconsolidar.vDescripcion
                                    Else
                                        'El usuario selecciono la configuracion predeterminada del producto
                                    End If
                                Else
                                    GoTo Fin
                                End If
                            Else
                                'If _Producto_RequiereDescripcion Then                                                    'REQUIERE DESCRIPCION
                                '   Dim piv_Descripcion As New form_PuntoDeVenta_RequiereDescripcion()
                                '   If piv_Descripcion.ShowDialog = DialogResult.OK Then
                                '      _Producto_DescripcionAdicional = piv_Descripcion.vf_Descripcion
                                '   Else
                                '      GoTo Fin
                                '   End If
                                'End If
                            End If
                            '
                            vf_Total_Costo_MovAlmacen = vf_Total_Costo_MovAlmacen + (_Producto_Cantidad * _Producto_PrecioCosto)
                            '
                            vf_GranTOTAL = vf_GranTOTAL + (_Producto_Cantidad * _Producto_PrecioVendido)
                            Label_GranTotal.Text = Format(vf_GranTOTAL, "###,###,##0.00")
                            '
                            If fun_GrabaVentasEn_tbVentasPVDetalle() Then                                                   'GRABA EN VentasPVDetalle
                                RecargaGridVentas()
                            Else
                                MessageBox.Show("Se ha producido un Error al Grabar en una Tabla Temporal. Pongase en contacto con su Soporte Tecnico")
                            End If
                            '
                            prog_InicializaVariablesXProducto()
                            txtLeerCodigo.Text = ""
                        Else                      'If fun_DatosProductoAVisualizar(_Producto_Codigo)
                            If fun_ProductosCoincidenConLaDescripcion(_Producto_CodigoBarras) = 1 Then                    'EL PRODUCTO EXISTE
                                _Producto_CodigoBarras = fun_ObtenerCodigoDelProducto(_Producto_CodigoBarras)
                                GoTo ObtenerDatosDelProducto
                            Else
                                GoTo LlamaCatalogoProducto_Isofacto
                            End If
                        End If
                    End If
                End If
            Else                                                      'if_13. Len(Trim(txt_LeerCodigo.Text)) <= 0
LlamaCatalogoProductos:
                _Producto_CodigoBarras = ""
                If InStr(1, Chr(13), e.KeyChar) > 0 Then    'Evita que llame al catalogo cuando se oprime <+>
                    vf_ContadorEnter = vf_ContadorEnter + 1
                    e.KeyChar = ""
                End If
                If vf_ContadorEnter >= 2 Then
                    prog_InicializaVariablesXProducto()
LlamaCatalogoProducto_Isofacto:
                    PanelTrabajoDerecho_DeshabilitarControles()
                    cCatProd = New ctrCatalogoProductos(_Producto_CodigoBarras)
                    Me.panelTrabajoDerecho.Controls.Add(cCatProd)
                    AddHandler cCatProd.CerrarCatalogoProductos, AddressOf CerrarCatalogoProductos
                    cCatProd.Dock = DockStyle.Fill
                    cCatProd.BringToFront()
                    'Dim pivote4 As New Catalogo(_Producto_CodigoBarras)                              'LLAMA AL CATALOGO DE PRODUCTOS
                    'If pivote4.ShowDialog() = DialogResult.OK Then
                    '    If _Producto_Cantidad = 1 Or _Producto_Cantidad = -1 Then
                    '        StatusStripPrincipal_Mensajes.Text = "Introduzca la Cantidad de " & pivote4.vf_ProductoSeleccionado_Nombre
                    '        txtLeerCodigo.Text = "*" & Trim(Str(pivote4.vf_ProductoSeleccionado_CodigoBarras))
                    '        My.Computer.Keyboard.SendKeys("{HOME}", True)
                    '    Else
                    '        txtLeerCodigo.Text = Str(_Producto_Cantidad) & "*" & Trim(Str(pivote4.vf_ProductoSeleccionado_CodigoBarras))
                    '        My.Computer.Keyboard.SendKeys("{ENTER}", True)
                    '    End If
                    '    '
                    '    Try
                    '        If Len(Trim(pivote4.vf_ProductoSeleccionado_Foto)) = 0 Then
                    '            btn_Foto.BackgroundImage = Nothing
                    '            btn_Foto.Text = Per_PV_Leyenda
                    '            _Producto_Foto = ""
                    '        Else
                    '            btn_Foto.BackgroundImage = Image.FromFile(gTerminalBD_Ruta_FotosProductos & "\" & pivote4.vf_ProductoSeleccionado_Foto)
                    '            btn_Foto.Text = ""
                    '            _Producto_Foto = pivote4.vf_ProductoSeleccionado_Foto
                    '        End If
                    '    Catch
                    '    End Try
                    'End If
                End If
            End If                             'if_13. Len(Trim(txt_LeerCodigo.Text)) > 0                                    COMPACTA VentasPVDetalle
            Compacta_RegistrosDelVisorDeVentas()
            MonederoElectronico_CalcularDetalle()                                                                                   'CALCULAR MONEDERO ELECTRONICO
            MonederoElectronico_CalcularTotal()
            RecargaGridVentas()
        Else                              'InStr(1, Chr(13) o <+>, e.KeyChar) > 0
            If InStr(1, "0123456789* _-/.#$%&()=ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz" & Chr(8), e.KeyChar) = 0 Then    'Si no es un caracter aceptado
                If InStr(1, Chr(27), e.KeyChar) > 0 Then                        'Si oprimio <ESC>
                    txtLeerCodigo.Text = ""
                    txtLeerCodigo.Clear()
                    StatusStripPrincipal_Mensajes.Text = "Introduzca el Código del Producto. O teclee la cantidad seguida por un asterisco y luego el Codigo"
                End If
                e.KeyChar = ""
            Else
                StatusStripPrincipal_Mensajes.Text = "Despues de Introducir la Cantidad y el Codigo oprima ENTER"
            End If
            vf_ContadorEnter = 0
        End If                            'InStr(1, Chr(13), e.KeyChar) > 0
        If InStr(1, "+", e.KeyChar) > 0 Then                                                                                'COBRANDO. Inicio
            '
            e.KeyChar = ""
            '
            If vf_ClienteYaSeleccionado = False And Per_PV_Cliente_ExigirCodigo = True Then
                MessageBox.Show("No ha seleccionado ningun Cliente")
                GoTo Fin
            End If
            '
            Obtener_NumLineas_GranTotal_tbVentasPVDetalle()
            '
            If vf_ContadorItems > 0 Or _Cliente_Credito_Usado > 0 Then
                If _ImportePositivo_Venta > 0 And _ImporteNegativo_Venta < 0 Then
                    'No se puede revolver venta con devoluciones
                    cLibreriasMensaje.Mostrar("No es posible hacer una devolucion si tiene una venta", Mensajes.Emergente.Advertencia)
                    txtLeerCodigo.Focus()
                Else
                    Try
                        _ImportePositivo_Venta = Math.Round(_ImportePositivo_Venta, 1)
                    Catch
                        MessageBox.Show("Fallo de redondeo")
                        _ImportePositivo_Venta = 0
                    End Try
                    '_Importe_Venta = Val(Replace(Label_GranTotal.Text, ",", ""))
                    '
                    If Not _ImporteYaSeCapturoElPagoDelCliente Then
                        _ImportePago_Efectivo = _ImportePositivo_Venta + _ImporteNegativo_Venta
                    End If
                    '
                    PanelTrabajoDerecho_DeshabilitarControles()
                    '
                    Try
                        _ctrCobrando = New Cobrando(labelClienteClave.Text,
                                                    labelClienteNombre.Text, _
                                                    _Cliente_FotoMasivaCliente, _
                                                    _Cliente_Credito_Aplica, _
                                                    _Cliente_Credito_Limite,
                                                    _Cliente_Credito_Usado, _
                                                    _Cliente_Credito_BloquearSiDebe, _
                                                    _Cliente_ME_Aplica, _
                                                    _Cliente_ME_Disponible, _
                                                    _Cliente_Bloqueado, _
                                                    _ImportePositivo_Venta, _
                                                    _ImporteNegativo_Venta, _
                                                    _ImportePago_Efectivo, _
                                                    _ImportePago_ME, _
                                                    _ImportePago_Credito, _
                                                    _ImportePago_Referencia, _
                                                    _ImportePago_TOTAL, _
                                                    _Importe_Vuelto, _
                                                    _Importe_AdeudoAPagar, _
                                                    cTarjeta.ME_ImportexEstaVenta,
                                                    Per_MonederoElectronico_Aplica, _
                                                    Per_Credito_Aplica, _
                                                    per_Credito_MostrarLimiteYDisponible, _
                                                    Per_MonederoElectronico_ClientesTodosOSegunPerfil, _
                                                    _Tarjeta_Numero, _
                                                    _Tarjeta_Existe, _
                                                    _Tarjeta_ClienteId, _
                                                    _Tarjeta_ME_Activada, _
                                                    _Tarjeta_ME_Disponible, _
                                                    _Tarjeta_CreditoActivada, _
                                                    _Tarjeta_CreditoLimite, _
                                                    _Tarjeta_CreditoUsado, _
                                                    _Tarjeta_CreditoDisponible, _
                                                    _Tarjeta_Cancelada, _ImporteYaSeCapturoElPagoDelCliente)
                        '_ctrMonElec.ObtenerDisponibleCliente(_Cliente_Id) + _ctrMonElec.ImportexEstaVenta
                        Me.panelTrabajoDerecho.Controls.Add(_ctrCobrando)
                        AddHandler _ctrCobrando.ClickBotonesTerminar, AddressOf Cobrando_EventCerrar
                        AddHandler _ctrCobrando.ClickBotonClientePendientes, AddressOf AdministradorPendientes
                        'AddHandler _ctrCobrando.MostrarMensajeEntreTarjeta, AddressOf Cobrando_MuestraMensajeEntrarTarjeta
                        _ctrCobrando.Dock = DockStyle.Fill
                        _ctrCobrando.BringToFront()
                        _ctrCobrando.txtCobrandoEfectivoRecibido.Focus()
                    Catch exe As Exception
                        'MessageBox.Show(labelClienteClave.Text & "-" & labelClienteNombre.Text & "-Imagen-" & _
                        '                            _Cliente_Credito_Aplica & "-" & _
                        '                            _Cliente_Credito_Limite & "-" & _
                        '                            _Cliente_Credito_Usado & "-" & _
                        '                            _Cliente_Credito_BloquearSiDebe & "-" & _
                        '                            _Cliente_ME_Aplica & "-" & _
                        '                            _Cliente_ME_Disponible & "-" & _
                        '                            _Cliente_Bloqueado & "-" & _
                        '                            _ImportePositivo_Venta & "-" & _
                        '                            _ImporteNegativo_Venta & "-" & _
                        '                            _ImportePago_Efectivo & "-" & _
                        '                            _ImportePago_ME & "-" & _
                        '                            _ImportePago_Credito & "-" & _
                        '                            _ImportePago_Referencia & "-" & _
                        '                            _ImportePago_TOTAL & "-" & _
                        '                            _Importe_Vuelto & "-" & _
                        '                            _Importe_AdeudoAPagar & "-" & _
                        '                            cTarjeta.ME_ImportexEstaVenta & "-" & _
                        '                            Per_MonederoElectronico_Aplica.ToString() & "-" & _
                        '                            Per_Credito_Aplica.ToString() & "-" & _
                        '                            per_Credito_MostrarLimiteYDisponible & "-" & _
                        '                            Per_MonederoElectronico_ClientesTodosOSegunPerfil & "-" & _
                        '                            _Tarjeta_Numero & "-" & _
                        '                            _Tarjeta_Existe.ToString() & "-" & _
                        '                            _Tarjeta_ClienteId & "-" & _
                        '                            _Tarjeta_ME_Activada.ToString() & "-" & _
                        '                            _Tarjeta_ME_Disponible & "-" & _
                        '                            _Tarjeta_CreditoActivada.ToString() & "-" & _
                        '                            _Tarjeta_CreditoLimite & "-" & _
                        '                            _Tarjeta_CreditoUsado & "-" & _
                        '                            _Tarjeta_CreditoDisponible & "-" & _
                        '                            _Tarjeta_Cancelada.ToString() & "-" & _ImporteYaSeCapturoElPagoDelCliente.ToString())
                    End Try
                End If
            Else
                cLibreriasMensaje.Mostrar("No existen productos capturados para Cobrar", Mensajes.Emergente.Advertencia)
                txtLeerCodigo.Focus()
            End If
        End If
        '
        If vf_ContadorEnter = 1 Then
            StatusStripPrincipal_Mensajes.Text = "Oprima Enter 2 veces para entrar al Catalogo de Productos"
        End If
Fin:
    End Sub    'txt_LeerCodigo_KeyPress
    Private Sub txtLeerCodigo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLeerCodigo.LostFocus
        Me.txtLeerCodigo.BackColor = Color.Green
    End Sub
    Private Sub txtLeerCodigo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLeerCodigo.Leave
        Me.txtLeerCodigo.BackColor = Color.Green
    End Sub
#End Region

#Region "btnCantidadCodigo"
    Private Sub btnCantidadCodigo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCantidadCodigo.Click
        If Me.btnCantidadCodigo.Size.Width = 20 Then
            Me.btnCantidadCodigo.Size = New Point(206, 41)
            Me.btnCantidadCodigo.Text = "Cantidad * Codigo"
        Else
            Me.btnCantidadCodigo.Size = New Point(20, 41)
            Me.btnCantidadCodigo.Text = "<"
        End If
    End Sub
    Private Sub btnCantidadCodigo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCantidadCodigo.GotFocus
        Me.txtLeerCodigo.Focus()
    End Sub
#End Region

#Region "Panel Demo"
    Private Sub PanelDemo_SetPositions()
        panloc = panel_demo.Location
        curloc = System.Windows.Forms.Cursor.Position
    End Sub
    Private Sub panel_demo_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles panel_demo.MouseDown
        Timer_ReubicarPanelDemo.Enabled = True
        Timer_ReubicarPanelDemo.Start()
        PanelDemo_SetPositions()
    End Sub
    Private Sub panel_demo_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles panel_demo.MouseUp
        Timer_ReubicarPanelDemo.Stop()
        PanelDemo_SetPositions()
    End Sub
    Private Sub lnk_EsUnDemo_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnk_EsUnDemo_1.LinkClicked
        Me.panelTrabajo.Controls.Add(_ctrAcercaDe)
        _ctrAcercaDe.Dock = DockStyle.Fill
        _ctrAcercaDe.BringToFront()
    End Sub
    Private Sub lnk_EsUnDemo_2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnk_EsUnDemo_2.LinkClicked
        Me.panelTrabajo.Controls.Add(_ctrAcercaDe)
        _ctrAcercaDe.Dock = DockStyle.Fill
        _ctrAcercaDe.BringToFront()
    End Sub
#End Region

#Region "Timer_ Tick"
    Private Sub Timer_ReubicarPanelDemo_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_ReubicarPanelDemo.Tick
        panel_demo.Location = panloc - curloc + System.Windows.Forms.Cursor.Position
        If panel_demo.Location.X < 0 Or panel_demo.Location.Y < 0 Or panel_demo.Location.X + 350 > Me.panelTrabajoDerecho.Size.Width Or panel_demo.Location.Y + 110 > Me.panelTrabajoDerecho.Size.Height Then
            panel_demo.Location = New Point((Me.panelTrabajoDerecho.Size.Width - 350) / 2, (Me.panelTrabajoDerecho.Size.Height - 110) / 2)
        End If
        PanelDemo_SetPositions()
    End Sub
    Private Sub Timer_LicenciaDeUso_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer_LicenciaDeUso.Tick
        Try
            prog_VerificaLicenciasDeUso()
            prog_VerificaSiLaTerminalTodaviaExiste()
        Catch
        End Try
    End Sub
    Private Sub Timer_ChecaPendientes_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_ChecaPendientes.Tick
        If vg_PF.fun_ExistenProductosQueCambiaronDePrecio > 0 Then
            Try
                'form_MuestraPendientes.ShowDialog()
            Catch
            End Try
        End If
    End Sub
    Private Sub Timer_ActualizaPantalla_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_ActualizaPantalla.Tick
        'btn_FechaActual.Text = FormatDateTime(vg_ConfigSistema_FechaActual, DateFormat.LongDate) & "  " & FormatDateTime(TimeOfDay, DateFormat.ShortTime)
        prog_VerificaSiHayQueHacerCorteDelDia()
    End Sub
    Private Sub timerParpadeaEtiquetas_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerParpadeaEtiquetas.Tick
        If Me.labelCreditoAdeudo.Visible Then
            Me.labelCreditoAdeudo.Visible = False
        Else
            Me.labelCreditoAdeudo.Visible = True
        End If
        '
        If _LetraAumentando Then
            _LetraTamano = _LetraTamano + 0.75
        Else
            _LetraTamano = _LetraTamano - 0.75
        End If
        '
        If _LetraTamano > 15 Then
            _LetraAumentando = False
        Else

        End If
        '
        If _LetraTamano < 9 Then
            _LetraAumentando = True
        End If
        '
        Me.labelCreditoAdeudo.Font = New Font("Microsoft Sans Serif", _LetraTamano, FontStyle.Bold)
    End Sub
#End Region

#Region "dgv_Ventas"
    Private Sub dgv_Ventas_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv_Ventas.CellClick
        If dgv_Ventas.Rows.Count = 0 Then
            GoTo fin
        End If
        '
        Dim cell As DataGridViewCell = dgv_Ventas.CurrentCell
        '
        Select Case cell.ColumnIndex
            Case Is = 0     'Llamar al Panel de Negociacion
                PanelNegociacion_Llamar()
            Case Is = 1     'Editar Datos del Producto
                Try
                    Dim Pivote2 As New Productos_NuevoEditar("EDITAR", Trim(dgv_Ventas.SelectedCells(33).Value.ToString()))
                    If Pivote2.ShowDialog = DialogResult.OK Then
                        '
                    End If
                Catch RED As Exception
                    MessageBox.Show("dgv_Ventas_CellClick. Error: " & RED.Message.ToString())
                End Try
            Case Is = 2     'Eliminar el producto del Visor
                If gUsuario_PV_HabilitarBotonCancelar Then
                    If gUsuario_PV_CancelarSinFirmar Then
                        vf_VentaCancelada_Justificacion = "No se le requirio Justificacion"
                        If MsgBox("Desea Eliminar el Registro ?", MsgBoxStyle.DefaultButton1 Or MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
                            prog_RegistraEnBitacora_RegistroEliminado()
                            prog_EliminarRegistro()
                            RecargaGridVentas()
                        End If
                    Else
                        Dim pivote As New form_VentasCanceladas_Justificacion()
                        If pivote.ShowDialog = DialogResult.OK Then
                            vf_VentaCancelada_Justificacion = pivote.vf_Descripcion
                            prog_RegistraEnBitacora_RegistroEliminado()
                            prog_EliminarRegistro()
                            RecargaGridVentas()
                        End If
                    End If
                Else
                    MessageBox.Show("NO TIENE PERMISOS PARA CANCELAR LA VENTA. " & Chr(13) & _
                                    "Para devolver el Producto teclee el signo negativo y el codigo del Producto." & Chr(13) & Chr(13) & _
                                    "Ejemplo 1:   -59 ( -  = Cantidad a devolver: 1, 59 = Codigo del Producto)" & Chr(13) & _
                                    "Ejemplo 2: -3*59 ( -3 = Cantidad a devolver: 3, 59 = Codigo del Producto")
                End If
                '
        End Select
fin:
    End Sub
    Private Sub dgv_Ventas_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dgv_Ventas.KeyPress
        If InStr(1, "+", e.KeyChar) > 0 Then
            txtLeerCodigo.Focus()
            My.Computer.Keyboard.SendKeys("{+}", True)
        End If
    End Sub
    Private Sub dgv_Ventas_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles dgv_Ventas.RowPrePaint
        'Select Case dgv_Ventas.Rows(e.RowIndex).Cells("Usuario").Value.ToString
        '   Case String.Empty
        'dgv_Ventas.Rows(e.RowIndex).Cells("Operacion").Style.ForeColor = Color.Red
        '  Case "ADMINSTRADOR"
        'dgv_Ventas.Rows(e.RowIndex).Cells("Usuario").Style.ForeColor = Color.Blue
        ' Case Else
        'dgv_Ventas.Rows(e.RowIndex).Cells("Usuario").Style.ForeColor = Color.Black
        'End Select
    End Sub
    Private Sub dgv_Ventas_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgv_Ventas.SelectionChanged
        Try
            btn_FotoProducto.BackgroundImage = cLibreriasImagen.StringToImage(dgv_Ventas.SelectedCells(IdxFoto).Value.ToString())
            '
            If btn_FotoProducto.BackgroundImage Is Nothing Then
                btn_FotoProducto.BackgroundImage = cLibreriasImagen.StringToImage(gPersonalizacion("Gen_FotoProductoX").ToString())
            End If
            'btn_Foto.BackgroundImage = Image.FromFile(Trim(gTerminalBD_Ruta_FotosProductos & "\" & Trim(dgv_Ventas.SelectedCells(30).Value.ToString())))
            'btn_Foto.Text = ""
        Catch
            'Try
            '    btn_FotoProducto.BackgroundImage = cLibreriasImagen.StringToImage(gPersonalizacion("Gen_FotoProductoX").ToString())
            'Catch
            '    MessageBox.Show("Se ha eliminado el archivo '" & gTerminalBD_Ruta_FotosProductos & "\Blanco.ico'. Pongase en contacto con su Soporte Tecnico")
            'End Try
        End Try
    End Sub
#End Region

#Region "Monedero Electronico"
    Private Sub MonederoElectronico_CalcularDetalle()
        '
        'Dim con As New SqlConnection
        'Dim cmd As New SqlCommand
        '
        Dim FactorPrincipal As String = Str(Per_MonederoElectronico_FactorPrincipal / 100)
        Dim SiImporteMEExcedeUtilidadEn As String = Str(Per_MonederoElectronico_SiImporteMEExcedeUtilidadEn)
        Dim AplicaEsteFactorAlterno As String = Str(Per_MonederoElectronico_AplicaEsteFactorAlterno / 100)
        '
        'Segun Perfil del cliente
        If _Cliente_ME_Aplica And Not _Cliente_Bloqueado Then   'Si el cliente aplica para ME y no esta bloqueado
            FactorPrincipal = Str(_Cliente_ME_FactorPrincipal / 100)
            SiImporteMEExcedeUtilidadEn = Str(_Cliente_ME_SiImporteMEExcedeUtilidadEn)
            AplicaEsteFactorAlterno = Str(_Cliente_ME_AplicaEsteFactorAlterno / 100)
        Else
            If _Tarjeta_Existe And _Tarjeta_ME_Activada Then    'Si la tarjeta existe y esta activada para ME
                FactorPrincipal = Str(_Tarjeta_ME_FactorPrincipal / 100)
                SiImporteMEExcedeUtilidadEn = Str(_Tarjeta_ME_SiImporteMEExcedeUtilidadEn)
                AplicaEsteFactorAlterno = Str(_Tarjeta_ME_AplicaEsteFactorAlterno / 100)
            Else
                'Parametros default
            End If
        End If
        '
        cLibreriasBD.InsertUpdate("UPDATE VentasPV_Detalle SET " & _
                              "   MonederoElectronico_Factor = " & _
                              "      (CASE WHEN (   (  (PUVendido * CantidadSurtida * " & FactorPrincipal & " * 100)/((PUVendido - PUCosto) * CantidadSurtida)  ) > " & SiImporteMEExcedeUtilidadEn & "     ) " & _
                              "            THEN " & AplicaEsteFactorAlterno * 100 & _
                              "            ELSE " & FactorPrincipal * 100 & _
                              "       END), " & _
                              "   MonederoElectronico_Importe = " & _
                              "      (CASE WHEN (   (  (PUVendido * CantidadSurtida * " & FactorPrincipal & " * 100)/((PUVendido - PUCosto) * CantidadSurtida)  ) >= " & SiImporteMEExcedeUtilidadEn & "     ) " & _
                              "            THEN (PUVendido * CantidadSurtida * " & AplicaEsteFactorAlterno & ") " & _
                              "            ELSE (PUVendido * CantidadSurtida * " & FactorPrincipal & ") " & _
                              "       END) " & _
                              " WHERE MonederoElectronico_Aplica = 1 AND MonederoElectronico_FactorProducto = 0 AND NivelDetalle = 0 AND TerminalId = " & Str(gTerminalXML_Id))
        'Try
        '    con.ConnectionString = vg_ConexionBD_Principal
        '    con.Open()
        '    cmd.Connection = con
        '    cmd.CommandText = "UPDATE VentasPV_Detalle SET " & _
        '                      "   MonederoElectronico_Factor = " & _
        '                      "      (CASE WHEN (   (  (PUVendido * CantidadSurtida * " & FactorPrincipal & " * 100)/((PUVendido - PUCosto) * CantidadSurtida)  ) > " & SiImporteMEExcedeUtilidadEn & "     ) " & _
        '                      "            THEN " & AplicaEsteFactorAlterno * 100 & _
        '                      "            ELSE " & FactorPrincipal * 100 & _
        '                      "       END), " & _
        '                      "   MonederoElectronico_Importe = " & _
        '                      "      (CASE WHEN (   (  (PUVendido * CantidadSurtida * " & FactorPrincipal & " * 100)/((PUVendido - PUCosto) * CantidadSurtida)  ) >= " & SiImporteMEExcedeUtilidadEn & "     ) " & _
        '                      "            THEN (PUVendido * CantidadSurtida * " & AplicaEsteFactorAlterno & ") " & _
        '                      "            ELSE (PUVendido * CantidadSurtida * " & FactorPrincipal & ") " & _
        '                      "       END) " & _
        '                      " WHERE MonederoElectronico_Aplica = 1 AND MonederoElectronico_FactorProducto = 0 AND NivelDetalle = 0 AND TerminalId = " & Str(gTerminalXML_Id)
        '    cmd.ExecuteNonQuery()
        '    con.Close()
        'Catch RED As Exception
        '    MessageBox.Show("MonederoElectronico_CalcularDetalle(). Error: " & RED.Message.ToString())
        'End Try
    End Sub
    'Private Sub ___MonederoElectronico_OfuscarDetalle()
    '    Dim x As String = Str(_Cliente_ME_Factor / 100)
    '    Try
    '        Dim VL_SqlConnection As New SqlConnection(vg_ConexionBD_Principal)
    '        Dim VL_ComandoSql As String = "UPDATE VentasPV_Detalle SET MonederoElectronico_Factor = 0, " & _
    '                                      "                              MonederoElectronico_Importe = 0" & _
    '                                      "   WHERE Nivel = 0 AND TerminalId = " & Str(TerminalXML_Id)
    '        Dim da_ As New SqlDataAdapter(VL_ComandoSql, VL_SqlConnection)
    '        Dim ds_ As New DataSet
    '        da_.Fill(ds_)
    '    Catch RED As Exception
    '        MessageBox.Show("MonederoElectronico_OfuscarDetalle(). Error: " & RED.Message.ToString())
    '    End Try
    'End Sub

    Private Sub MonederoElectronico_CalcularTotal()
        Try
            Dim conexion As New SqlConnection()
            conexion.ConnectionString = vg_ConexionBD_Principal
            Dim comando As New SqlCommand()
            comando.CommandText = "SELECT  SUM(MonederoElectronico_Importe) AS ImporteMonedero " & _
                                  "FROM VentasPV_Detalle " & _
                                  "WHERE MonederoElectronico_Aplica = 1 AND NivelDetalle = 0 AND TerminalId = " & Str(gTerminalXML_Id)
            comando.Connection = conexion
            conexion.Open()
            Dim dr As SqlDataReader = comando.ExecuteReader()
            If dr.HasRows Then
                While dr.Read()
                    cTarjeta.ME_ImportexEstaVenta = Val(dr("ImporteMonedero").ToString())
                End While
            End If
            dr.Close()
        Catch RED As Exception
            MessageBox.Show("MonederoElectronico_CalcularTotal(). Error " & RED.Message.ToString())
        End Try
    End Sub
#End Region

#Region "SubProgramas y Funciones"
    Private Function ClienteExiste(ByVal pClienteId As String) As Boolean
        pClienteId = Val(pClienteId).ToString()
        If pClienteId >= "001" And pClienteId <= "999" Then
            If cLibreriasBD.SelectFrom("SELECT * FROM clientes WHERE id = " & pClienteId) Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Private Sub PanelNegociacion_Llamar()
        '
        'VentasDelVisorDeVentas_Compacta()
        '
        Timer_ActualizaPantalla.Stop() 'Detener Timer para no interferir en las transacciones del SQL
        Timer_ActualizaPantalla.Enabled = False
        Try
            Dim Pivote As New form_PanelDeNegociacion(Trim(dgv_Ventas.SelectedCells(8).Value.ToString()), _
                                                                Trim(dgv_Ventas.SelectedCells(33).Value.ToString()), _
                                                                Trim(dgv_Ventas.SelectedCells(5).Value.ToString()), _
                                                                Trim(dgv_Ventas.SelectedCells(6).Value.ToString()), _
                                                                Trim(dgv_Ventas.SelectedCells(7).Value.ToString()), _
                                                                Trim(dgv_Ventas.SelectedCells(4).Value.ToString()), _
                                                                Trim(dgv_Ventas.SelectedCells(31).Value.ToString()), _
                                                                Trim(dgv_Ventas.SelectedCells(11).Value.ToString()), _
                                                                Trim(dgv_Ventas.SelectedCells(12).Value.ToString()), _
                                                                Trim(dgv_Ventas.SelectedCells(32).Value.ToString()))
            If Pivote.ShowDialog() = DialogResult.OK Then
                Dim currentCellAddress As Point = dgv_Ventas.CurrentCellAddress
                RecargaGridVentas()
                dgv_Ventas.CurrentCell = dgv_Ventas.Rows(currentCellAddress.Y).Cells(currentCellAddress.X)  'Posiciono el cursor
            End If
        Catch RED As Exception
            MessageBox.Show("PanelNegociacion_Llamar(). Error: " & RED.Message.ToString())
        End Try
        Timer_ActualizaPantalla.Start()
        Timer_ActualizaPantalla.Enabled = True
    End Sub
    Private Sub Compacta_RegistrosDelVisorDeVentas()
        cLibreriasBD.InsertUpdate("INSERT INTO VentasPV_Detalle (" & _
                                          "   TerminalId, " & _
                                          "   NivelDetalle, " & _
                                          "   ProductoId, " & _
                                          "   DesconsolidarEn, " & _
                                          "   DescripcionProducto, " & _
                                          "   PUCosto, " & _
                                          "   PUVenta, " & _
                                          "   PUVendido, " & _
                                          "   CantidadSurtida, " & _
                                          "   MonederoElectronico_Aplica, " & _
                                          "   MonederoElectronico_Factor, " & _
                                          "   MonederoElectronico_Importe, " & _
                                          "   MonederoElectronico_FactorProducto, " & _
                                          "   RequiereDescripcion, " & _
                                          "   DescripcionAdicionalProducto," & _
                                          "   Comodin) " & _
                                          "SELECT " & _
                                          "   TerminalId, " & _
                                          "   5, " & _
                                          "   ProductoId, " & _
                                          "   DesconsolidarEn, " & _
                                          "   DescripcionProducto, " & _
                                          "   PUCosto, " & _
                                          "   PUVenta, " & _
                                          "   (Sum(PUVendido * CantidadSurtida) / Sum(CantidadSurtida)) AS Promedio, " & _
                                          "   Sum(CantidadSurtida) AS TotalCantidad, " & _
                                          "   MonederoElectronico_Aplica, " & _
                                          "   MonederoElectronico_Factor, " & _
                                          "   MonederoElectronico_Importe, " & _
                                          "   MonederoElectronico_FactorProducto, " & _
                                          "   RequiereDescripcion, " & _
                                          "   DescripcionAdicionalProducto," & _
                                          "   Min(CR) as Ordenado " & _
                                          "FROM VentasPV_Detalle " & _
                                          "WHERE NivelDetalle = 0 AND TerminalId = " & Str(gTerminalXML_Id) & " " & _
                                          "GROUP BY ProductoId, TerminalId, " & _
                                          "         DesconsolidarEn, DescripcionProducto, PUCosto, " & _
                                          "         PUVenta, MonederoElectronico_Aplica, MonederoElectronico_Factor, " & _
                                          "         MonederoElectronico_Importe, MonederoElectronico_FactorProducto, " & _
                                          "         RequiereDescripcion, " & _
                                          "         DescripcionAdicionalProducto " & _
                                          "ORDER BY Ordenado;" & _
                                          "  " & _
                                          "; " & _
                                          "  " & _
                                          "DELETE FROM VentasPV_Detalle WHERE NivelDetalle = 0 AND TerminalId = " & Str(gTerminalXML_Id) & _
                                          "  " & _
                                          "; " & _
                                          "  " & _
                                          "UPDATE VentasPV_Detalle SET NivelDetalle = 0 WHERE NivelDetalle = 5 AND TerminalId = " & Str(gTerminalXML_Id))
        'Try
        '    Dim VL_SqlConnection As New SqlConnection(vg_ConexionBD_Principal)
        '    Dim VL_ComandoSql As String = "INSERT INTO VentasPV_Detalle (" & _
        '                                  "   TerminalId, " & _
        '                                  "   NivelDetalle, " & _
        '                                  "   ProductoId, " & _
        '                                  "   DesconsolidarEn, " & _
        '                                  "   DescripcionProducto, " & _
        '                                  "   PUCosto, " & _
        '                                  "   PUVenta, " & _
        '                                  "   PUVendido, " & _
        '                                  "   CantidadSurtida, " & _
        '                                  "   MonederoElectronico_Aplica, " & _
        '                                  "   MonederoElectronico_Factor, " & _
        '                                  "   MonederoElectronico_Importe, " & _
        '                                  "   MonederoElectronico_FactorProducto, " & _
        '                                  "   RequiereDescripcion, " & _
        '                                  "   DescripcionAdicionalProducto," & _
        '                                  "   Comodin) " & _
        '                                  "SELECT " & _
        '                                  "   TerminalId, " & _
        '                                  "   5, " & _
        '                                  "   ProductoId, " & _
        '                                  "   DesconsolidarEn, " & _
        '                                  "   DescripcionProducto, " & _
        '                                  "   PUCosto, " & _
        '                                  "   PUVenta, " & _
        '                                  "   (Sum(PUVendido * CantidadSurtida) / Sum(CantidadSurtida)) AS Promedio, " & _
        '                                  "   Sum(CantidadSurtida) AS TotalCantidad, " & _
        '                                  "   MonederoElectronico_Aplica, " & _
        '                                  "   MonederoElectronico_Factor, " & _
        '                                  "   MonederoElectronico_Importe, " & _
        '                                  "   MonederoElectronico_FactorProducto, " & _
        '                                  "   RequiereDescripcion, " & _
        '                                  "   DescripcionAdicionalProducto," & _
        '                                  "   Min(CR) as Ordenado " & _
        '                                  "FROM VentasPV_Detalle " & _
        '                                  "WHERE NivelDetalle = 0 AND TerminalId = " & Str(gTerminalXML_Id) & " " & _
        '                                  "GROUP BY ProductoId, TerminalId, " & _
        '                                  "         DesconsolidarEn, DescripcionProducto, PUCosto, " & _
        '                                  "         PUVenta, MonederoElectronico_Aplica, MonederoElectronico_Factor, " & _
        '                                  "         MonederoElectronico_Importe, MonederoElectronico_FactorProducto, " & _
        '                                  "         RequiereDescripcion, " & _
        '                                  "         DescripcionAdicionalProducto " & _
        '                                  "ORDER BY Ordenado;" & _
        '                                  "  " & _
        '                                  "; " & _
        '                                  "  " & _
        '                                  "DELETE FROM VentasPV_Detalle WHERE NivelDetalle = 0 AND TerminalId = " & Str(gTerminalXML_Id) & _
        '                                  "  " & _
        '                                  "; " & _
        '                                  "  " & _
        '                                  "UPDATE VentasPV_Detalle SET NivelDetalle = 0 WHERE NivelDetalle = 5 AND TerminalId = " & Str(gTerminalXML_Id)
        '    Dim da_ As New SqlDataAdapter(VL_ComandoSql, VL_SqlConnection)
        '    Dim ds_ As New DataSet
        '    da_.Fill(ds_)
        'Catch RED As Exception
        '    MessageBox.Show("Compacta_RegistrosDelVisorDeVentas(). Error: " & RED.Message.ToString())
        'End Try
    End Sub

    Private Sub prog_MuestraOcultaBotones()
        'If gUsuario_Admin_PanelAdministrador Then
        '    btn_PanelDeControl.Visible = True
        'Else
        '    btn_PanelDeControl.Visible = False
        'End If
        '
        If gUsuario_PanelNegociacion_NegociarPrecios Then
            dgv_Ventas.Columns(0).Visible = True
        Else
            dgv_Ventas.Columns(0).Visible = False
        End If

        '
        If gUsuario_Admin_MovAlmacen Then
            btn_MovAlmacen.Visible = True
        Else
            btn_MovAlmacen.Visible = False
        End If
        '
        If gUsuario_Admin_Productos Then
            'btn_NuevoProducto.Visible = True
            dgv_Ventas.Columns(1).Visible = True
        Else
            'btn_NuevoProducto.Visible = False
            dgv_Ventas.Columns(1).Visible = False
        End If
        '
        If gUsuario_PV_VentasDelDia Then
            'btn_VentasAnteriores.Visible = True
        Else
            'btn_VentasAnteriores.Visible = False
        End If
    End Sub
    Private Sub prog_EliminarRegistro()
        Try
            Dim VL_SqlConnection As New SqlConnection(vg_ConexionBD_Principal)
            Dim VL_ComandoSql As String = "DELETE FROM VentasPV_Detalle" & _
                " WHERE CR = " & Trim(dgv_Ventas.SelectedCells(8).Value.ToString())
            Dim da_CatalogoProductos As New SqlDataAdapter(VL_ComandoSql, VL_SqlConnection)
            Dim ds_CatalogoProductos As New DataSet
            da_CatalogoProductos.Fill(ds_CatalogoProductos)
        Catch RED As Exception
            MessageBox.Show("prog_EliminarRegistro(). Error: " & RED.Message.ToString())
        End Try
    End Sub
    Private Sub prog_ObtenerCantidadCodigo(ByRef p_Cadena As String, ByRef p_Cantidad As String, ByRef p_CodigoBarrasProducto As String)
        Dim vCantidad As String = "1"
        vf_PosicionAsterisco = InStr(1, p_Cadena, "*")
        If vf_PosicionAsterisco > 1 Then
            If Mid(p_Cadena, 1, 1) = "-" Then
                If Mid(p_Cadena, 2, 1) = "*" Then
                    vCantidad = -1
                Else
                    vCantidad = Mid(Trim(p_Cadena), 1, vf_PosicionAsterisco - 1)
                End If
            Else
                vCantidad = Mid(Trim(p_Cadena), 1, vf_PosicionAsterisco - 1)
            End If
            p_CodigoBarrasProducto = Mid(Trim(p_Cadena), vf_PosicionAsterisco + 1)
        Else
            If vf_PosicionAsterisco = 0 Then
                If Mid(Trim(p_Cadena), 1, 1) = "-" Then
                    vCantidad = -1
                    p_Cadena = Mid(p_Cadena, 2)
                End If
            Else                             'vl_PosicionAsterisco = 1
                vCantidad = 1
                p_Cadena = Mid(p_Cadena, 2)
            End If
            p_CodigoBarrasProducto = p_Cadena
        End If
        '*-.#$%&/()=
        vCantidad = Replace(vCantidad, "*", "")
        vCantidad = Replace(vCantidad, "#", "")
        vCantidad = Replace(vCantidad, "$", "")
        vCantidad = Replace(vCantidad, "%", "")
        vCantidad = Replace(vCantidad, "&", "")
        vCantidad = Replace(vCantidad, "/", "")
        vCantidad = Replace(vCantidad, "(", "")
        vCantidad = Replace(vCantidad, ")", "")
        vCantidad = Replace(vCantidad, "=", "")
        p_Cantidad = Val(vCantidad)
        'p_CodigoBarrasProducto = Replace(p_CodigoBarrasProducto, "*", "")
    End Sub             'prog_ObtenerCantidadCodigo
    Private Sub ObtenerDatosClienteAPartirDeTarjeta()
        '
        InicializaDatosTarjeta()
        prog_ObtenerDatosClienteAVisualizar(Per_PV_Cliente_IDDefault)
        '
        If Mid(Trim(txtLeerCodigo.Text), 8, 6) = String.Empty Then
            cLibreriasMensaje.Mostrar("Numero de Tarjeta NO Valida !!!", Mensajes.Emergente.Advertencia)
        Else
            _Tarjeta_Numero = Mid(Trim(txtLeerCodigo.Text), 8, 6)
            '_Tarjeta_Id = cTarjeta.Tarjeta_ObtenerId(_Tarjeta_Numero)
            cTarjeta.EstatusDeLaTarjeta(_Tarjeta_Numero, _Tarjeta_Id, _Tarjeta_Existe, _Tarjeta_ClienteId, _Tarjeta_ME_Activada, _
                                        _Tarjeta_ME_FactorPrincipal, _Tarjeta_ME_SiImporteMEExcedeUtilidadEn, _Tarjeta_ME_AplicaEsteFactorAlterno, _
                                        _Tarjeta_ME_Disponible, _
                                        _Tarjeta_CreditoActivada, _Tarjeta_CreditoLimite, _Tarjeta_CreditoUsado, _Tarjeta_CreditoDisponible, _Tarjeta_Cancelada)
            '
            If _Tarjeta_Existe Then
                If Not _Tarjeta_Cancelada Then
                    If _Tarjeta_ME_Activada Or _Tarjeta_CreditoActivada Then
                        If _Tarjeta_ClienteId = 0 Then                    'Si la tarjeta no tiene un cliente asociado, le asigna al cliente Predeterminado
                            _Cliente_Id = Per_PV_Cliente_IDDefault
                        Else
                            _Cliente_Id = _Tarjeta_ClienteId
                        End If
                        '
                        prog_ObtenerDatosClienteAVisualizar(_Cliente_Id)
                        '
                        label_RangoPrecios.Text = _Cliente_Precios_RangoAplicado
                        If _Cliente_Precios_RangoAplicado = "% por Definir" Then
                            label_RangoPrecios.Text = label_RangoPrecios.Text & ": " & fun_FormatoCantidad(_Cliente_Precios_PorcentADefinir) & " %"
                        End If
                        '
                        label_TarjetaSiNo.Visible = True
                        label_TarjetaSiNo.Text = "Tarjeta No.: *****" & Mid(Trim(Str(_Tarjeta_Numero)), 1, 1)
                    Else
                        cLibreriasMensaje.Mostrar("La tarjeta no esta activada para Acumular Dinero Electronico", Mensajes.Emergente.Error)
                        Me.txtLeerCodigo.Focus()
                    End If
                Else
                    cLibreriasMensaje.Mostrar("La Tarjeta se Encuentra Cancelada", Mensajes.Emergente.Error)
                    Me.txtLeerCodigo.Focus()
                End If
            Else
                cLibreriasMensaje.Mostrar("Tarjeta NO VALIDA. No existe en el Sistema", Mensajes.Emergente.Advertencia)
                Me.txtLeerCodigo.Focus()
            End If
        End If
        '
        txtLeerCodigo.Text = _Tarjeta_Numero
    End Sub
    Private Sub prog_InicializaVariablesXProducto()
        vf_PosicionAsterisco = 0
        _Producto_Cantidad = 1
        _Producto_CodigoBarras = ""
        vf_ContadorEnter = 0
        '_Producto_Codigo = ""
        '_Producto_Descripcion = ""
        _Producto_DescripcionAdicional = ""
        _Producto_PrecioCosto = 0
        _Producto_PrecioVentaMenudeo = 0
        _Producto_PrecioVendido = 0
        _Producto_DesconEn = 1
        '
        _Producto_MedioMayoreo_Aplica = False
        _Producto_MedioMayoreo_Precio = 0
        _Producto_MedioMayoreo_CantidadMinima = 0
        _Producto_Mayoreo_Aplica = False
        _Producto_Mayoreo_Precio = 0
        _Producto_Mayoreo_CantidadMinima = 0
        _Producto_GranMayoreo_Aplica = False
        _Producto_GranMayoreo_Precio = 0
        _Producto_GranMayoreo_CantidadMinima = 0
        '
        _Producto_Embalaje = ""
        _Producto_CantidadDisponible = 0
        _Producto_RequiereDescripcion = False
        _Producto_NoPuedeSePuedeVenderSinoDisponible = False
        '
        _Producto_ME_Aplica = False
        _Producto_ME_MenudeoAplicaSuPropioFactor = False
        _Producto_ME_MenudeoFactor = 0
        _Producto_ME_MedioMayAplicaSuPropioFactor = False
        _Producto_ME_MedioMayFactor = 0
        _Producto_ME_MayoreoAplicaSuPropioFactor = False
        _Producto_ME_MayoreoFactor = 0
        _Producto_ME_GranMayAplicaSuPropioFactor = False
        _Producto_ME_GranMayFactor = 0
        '
        ProductoTieneImagen = False
    End Sub
    Private Sub prog_InicializaVariablesXVenta()
        txtLeerCodigo.Text = ""
        Label_GranTotal.Text = "0.00"
        _ImportePositivo_Venta = 0
        _ImporteNegativo_Venta = 0
        _ImportePago_Efectivo = 0
        _ImportePago_ME = 0
        _ImportePago_Credito = 0
        '_ImportePago_OtroMedio = 0
        _ImportePago_Referencia = ""
        _ImportePago_TOTAL = 0
        _Importe_Vuelto = 0
        _Importe_AdeudoAPagar = 0
        _ImporteYaSeCapturoElPagoDelCliente = False
        vf_ContadorItems = 0
        vf_Total_Costo_MovAlmacen = 0
        vf_GranTOTAL = 0
        cTarjeta.ME_ImportexEstaVenta = 0
        vf_Ventas_FolioAGrabar = 0
        vf_MovAlmacen_FolioAGrabar = 0
        vf_ClienteYaSeleccionado = False
        'Cliente
        _Tarjeta_Numero = ""
        _Tarjeta_Id = 0
        _Tarjeta_ME_Activada = False
        _Tarjeta_ME_Disponible = 0
        _Tarjeta_CreditoActivada = False
        _Tarjeta_CreditoDisponible = 0
        '
        Me.timerParpadeaEtiquetas.Stop()
        Me.timerParpadeaEtiquetas.Enabled = False
        '
        Me.btn_FotoProducto.BackgroundImage = cLibreriasImagen.StringToImage(gPersonalizacion("Gen_FotoProductoX").ToString())
        '
        prog_ObtenerDatosClienteAVisualizar(Per_PV_Cliente_IDDefault)
    End Sub
    Private Function fun_ExisteUnaVentaGuardada()
        Dim Existe As Boolean = False
        Try
            Dim conexion As New SqlConnection()
            conexion.ConnectionString = vg_ConexionBD_Principal
            Dim comando As New SqlCommand()
            comando.CommandText = "SELECT * FROM VentasPV_Detalle WHERE NIvelDetalle = 1" & " AND TerminalId = " & Str(gTerminalXML_Id)
            comando.Connection = conexion
            conexion.Open()
            Dim dr As SqlDataReader = comando.ExecuteReader()
            If dr.HasRows Then
                While dr.Read()
                    Existe = True
                End While
            End If
            dr.Close()
        Catch RED As Exception
            MessageBox.Show("fun_ExisteUnaVentaGuardada(). Error " & RED.Message.ToString())
        End Try
        Return Existe
    End Function
    Private Sub prog_CambiaA_1_ElFolioDeVentaTemporal()
        Try
            Dim VL_SqlConnection As New SqlConnection(vg_ConexionBD_Principal)
            Dim VL_ComandoSql As String = "UPDATE VentasPV_Detalle SET NivelDetalle = 1 WHERE NivelDetalle = 0 " & _
                                          "AND TerminalId = " & Str(gTerminalXML_Id)
            Dim da_CatalogoProductos As New SqlDataAdapter(VL_ComandoSql, VL_SqlConnection)
            Dim ds_CatalogoProductos As New DataSet
            da_CatalogoProductos.Fill(ds_CatalogoProductos)
        Catch RED As Exception
            MessageBox.Show("prog_CambiaA_1_ElFolioDeVentaTemporal(). Error: " & RED.Message.ToString())
        End Try
    End Sub
    Private Sub prog_CambiaA_0_ElFolioDeVentaTemporal()
        Try
            Dim VL_SqlConnection As New SqlConnection(vg_ConexionBD_Principal)
            Dim VL_ComandoSql As String = "UPDATE VentasPV_Detalle SET NivelDetalle = 0 WHERE NivelDetalle = 1 " & _
                                          "AND TerminalId = " & Str(gTerminalXML_Id)
            Dim da_CatalogoProductos As New SqlDataAdapter(VL_ComandoSql, VL_SqlConnection)
            Dim ds_CatalogoProductos As New DataSet
            da_CatalogoProductos.Fill(ds_CatalogoProductos)
        Catch RED As Exception
            MessageBox.Show("prog_CambiaA_0_ElFolioDeVentaTemporal(). Error: " & RED.Message.ToString())
        End Try
    End Sub
    Private Function fun_DatosProductoAVisualizarMain(ByVal p_CodigoBarrasProducto As Double) As Boolean
        Dim vl_DatosOk As Boolean = False
        Try
            Dim conexion As New SqlConnection()
            conexion.ConnectionString = vg_ConexionBD_Principal
            Dim comando As New SqlCommand()
            comando.CommandText = "SELECT p.ID, p.CodigoBarras, p.Descripcion AS ProductoDescripcion, p.PrecioCosto, " & _
                "p.EmbalajeId, p.PrecioVenta, p.CantidadActual, p.MedioMayoreo_Aplica, " & _
                "p.MedioMayoreo_PrecioVenta, p.MedioMayoreo_CantidadMinima, " & _
                "p.Mayoreo_Aplica, p.Mayoreo_PrecioVenta, p.Mayoreo_CantidadMinima, " & _
                "p.GranMayoreo_Aplica, p.GranMayoreo_PrecioVenta, p.GranMayoreo_CantidadMinima, " & _
                "Marcas.Descripcion AS MarcaDescripcion, SubClases.Descripcion AS SubClaseDescripcion, " & _
                "p.RequiereDescripcion, p.NoVenderSinoDisponible, p.Inactivo, ISNULL(FotoProducto,'') AS Fotografia, " & _
                "p.Descon_1_Aplica, p.Descon_1_DesconsolidarEn, p.Descon_1_EmbalajeId, p.Descon_1_Descripcion, " & _
                "p.Descon_1_PrecioCosto, p.Descon_1_Utilidad, p.Descon_1_PrecioVenta, p.Descon_1_AplicaEsquemaPreciosMayoreo, " & _
                "p.Descon_2_Aplica, p.Descon_2_DesconsolidarEn, p.Descon_2_EmbalajeId, p.Descon_2_Descripcion, " & _
                "p.Descon_2_PrecioCosto, p.Descon_2_Utilidad, p.Descon_2_PrecioVenta, p.Descon_2_AplicaEsquemaPreciosMayoreo, " & _
                "p.Descon_3_Aplica, p.Descon_3_DesconsolidarEn, p.Descon_3_EmbalajeId, p.Descon_3_Descripcion, " & _
                "p.Descon_3_PrecioCosto, p.Descon_3_Utilidad, p.Descon_3_PrecioVenta, p.Descon_3_AplicaEsquemaPreciosMayoreo, " & _
                "p.ME_Aplica, p.ME_MenudeoAplicaSuPropioFactor, p.ME_MenudeoFactor, p.ME_MedioMayAplicaSuPropioFactor, p.ME_MedioMayFactor, " & _
                "p.ME_MayoreoAplicaSuPropioFactor, p.ME_MayoreoFactor, p.ME_GranMayAplicaSuPropioFactor, p.ME_GranMayFactor " & _
                "FROM Productos p " & _
                "LEFT JOIN Marcas ON p.MarcaId = Marcas.Id " & _
                "LEFT JOIN SubClases ON p.SubClaseId = SubClases.Id " & _
                "WHERE CodigoBarras = " & Str(p_CodigoBarrasProducto)
            comando.Connection = conexion
            conexion.Open()
            Dim dr As SqlDataReader = comando.ExecuteReader()
            If dr.HasRows Then
                While dr.Read()
                    _Producto_Id = Val(dr("Id").ToString)
                    _Producto_Descripcion = dr("ProductoDescripcion").ToString
                    _Producto_PrecioCosto = Val(dr("PrecioCosto").ToString)
                    _Producto_PrecioVentaMenudeo = Val(dr("PrecioVenta").ToString)
                    _Producto_PrecioVendido = Val(dr("PrecioVenta").ToString)
                    _Producto_MedioMayoreo_Aplica = dr("MedioMayoreo_Aplica").ToString
                    _Producto_MedioMayoreo_Precio = Val(dr("MedioMayoreo_PrecioVenta").ToString)
                    _Producto_MedioMayoreo_CantidadMinima = Val(dr("MedioMayoreo_CantidadMinima").ToString)
                    _Producto_Mayoreo_Aplica = dr("Mayoreo_Aplica").ToString
                    _Producto_Mayoreo_Precio = Val(dr("Mayoreo_PrecioVenta").ToString)
                    _Producto_Mayoreo_CantidadMinima = Val(dr("Mayoreo_CantidadMinima").ToString)
                    _Producto_GranMayoreo_Aplica = dr("GranMayoreo_Aplica").ToString
                    _Producto_GranMayoreo_Precio = Val(dr("GranMayoreo_PrecioVenta").ToString)
                    _Producto_GranMayoreo_CantidadMinima = Val(dr("GranMayoreo_CantidadMinima").ToString)
                    _Producto_Embalaje = dr("EmbalajeId").ToString
                    _Producto_CantidadDisponible = Val(dr("CantidadActual").ToString)
                    _Producto_RequiereDescripcion = dr("RequiereDescripcion").ToString
                    _Producto_NoPuedeSePuedeVenderSinoDisponible = dr("NoVenderSinoDisponible").ToString
                    _Producto_Foto = dr("Fotografia").ToString
                    '
                    _Producto_Descon_1_Aplica = dr("Descon_1_Aplica").ToString
                    _Producto_Descon_1_DesconsolidarEn = Val(dr("Descon_1_DesconsolidarEn").ToString)
                    _Producto_Descon_1_IdEmbalaje = Val(dr("Descon_1_EmbalajeId").ToString)
                    _Producto_Descon_1_Descripcion = Trim(dr("Descon_1_Descripcion").ToString)
                    _Producto_Descon_1_PrecioCosto = Val(dr("Descon_1_PrecioCosto").ToString)
                    _Producto_Descon_1_Utilidad = Val(dr("Descon_1_Utilidad").ToString)
                    _Producto_Descon_1_PrecioVenta = Val(dr("Descon_1_PrecioVenta").ToString)
                    _Producto_Descon_1_AplicaEsquemaPreciosMayoreo = dr("Descon_1_AplicaEsquemaPreciosMayoreo").ToString
                    _Producto_Descon_2_Aplica = dr("Descon_2_Aplica").ToString
                    _Producto_Descon_2_DesconsolidarEn = Val(dr("Descon_2_DesconsolidarEn").ToString)
                    _Producto_Descon_2_IdEmbalaje = Val(dr("Descon_2_EmbalajeId").ToString)
                    _Producto_Descon_2_Descripcion = Trim(dr("Descon_2_Descripcion").ToString)
                    _Producto_Descon_2_PrecioCosto = Val(dr("Descon_2_PrecioCosto").ToString)
                    _Producto_Descon_2_Utilidad = Val(dr("Descon_2_Utilidad").ToString)
                    _Producto_Descon_2_PrecioVenta = Val(dr("Descon_2_PrecioVenta").ToString)
                    _Producto_Descon_2_AplicaEsquemaPreciosMayoreo = dr("Descon_2_AplicaEsquemaPreciosMayoreo").ToString
                    _Producto_Descon_3_Aplica = dr("Descon_3_Aplica").ToString
                    _Producto_Descon_3_DesconsolidarEn = Val(dr("Descon_3_DesconsolidarEn").ToString)
                    _Producto_Descon_3_IdEmbalaje = Val(dr("Descon_3_EmbalajeId").ToString)
                    _Producto_Descon_3_Descripcion = Trim(dr("Descon_3_Descripcion").ToString)
                    _Producto_Descon_3_PrecioCosto = Val(dr("Descon_3_PrecioCosto").ToString)
                    _Producto_Descon_3_Utilidad = Val(dr("Descon_3_Utilidad").ToString)
                    _Producto_Descon_3_PrecioVenta = Val(dr("Descon_3_PrecioVenta").ToString)
                    _Producto_Descon_3_AplicaEsquemaPreciosMayoreo = dr("Descon_3_AplicaEsquemaPreciosMayoreo").ToString
                    _Producto_ME_Aplica = dr("ME_Aplica").ToString()
                    _Producto_ME_MenudeoAplicaSuPropioFactor = dr("ME_MenudeoAplicaSuPropioFactor").ToString()
                    _Producto_ME_MenudeoFactor = dr("ME_MenudeoFactor").ToString()
                    _Producto_ME_MedioMayAplicaSuPropioFactor = dr("ME_MedioMayAplicaSuPropioFactor").ToString()
                    _Producto_ME_MedioMayFactor = dr("ME_MedioMayFactor").ToString()
                    _Producto_ME_MayoreoAplicaSuPropioFactor = dr("ME_MayoreoAplicaSuPropioFactor").ToString()
                    _Producto_ME_MayoreoFactor = dr("ME_MayoreoFactor").ToString()
                    _Producto_ME_GranMayAplicaSuPropioFactor = dr("ME_GranMayAplicaSuPropioFactor").ToString()
                    _Producto_ME_GranMayFactor = dr("ME_GranMayFactor").ToString()
                    '
                    vl_DatosOk = True
                End While
            End If
            dr.Close()
            '
            If _Cliente_Precios_RangoAplicado = "Solo Menudeo" Then
                'No hacemos nada, ya que esta asignado el precio de menudeo en el parrafo anterior
            Else
                If _Cliente_Precios_RangoAplicado = "Menudeo" Then
                    If _Producto_MedioMayoreo_Aplica And Per_Productos_MedioMayoreo_Aplica Then
                        If _Producto_Cantidad >= _Producto_MedioMayoreo_CantidadMinima Then
                            _Producto_PrecioVendido = _Producto_MedioMayoreo_Precio
                        End If
                    End If
                    If _Producto_Mayoreo_Aplica And Per_Productos_MedioMayoreo_Aplica Then
                        If _Producto_Cantidad >= _Producto_Mayoreo_CantidadMinima Then
                            _Producto_PrecioVendido = _Producto_Mayoreo_Precio
                        End If
                    End If
                    If _Producto_GranMayoreo_Aplica And Per_Productos_MedioMayoreo_Aplica Then
                        If _Producto_Cantidad >= _Producto_GranMayoreo_CantidadMinima Then
                            _Producto_PrecioVendido = _Producto_Mayoreo_Precio
                        End If
                    End If
                Else
                    If _Cliente_Precios_RangoAplicado = "Medio Mayorista" Then
                        If _Producto_MedioMayoreo_Aplica Then
                            _Producto_PrecioVendido = _Producto_MedioMayoreo_Precio
                        End If
                        '
                        If _Producto_ME_MedioMayAplicaSuPropioFactor Then
                            _Producto_ME_MenudeoFactor = _Producto_ME_MedioMayFactor
                        End If
                    Else
                        If _Cliente_Precios_RangoAplicado = "Mayorista" Then
                            If _Producto_Mayoreo_Aplica Then
                                _Producto_PrecioVendido = _Producto_Mayoreo_Precio
                            End If
                            '
                            If _Producto_ME_MayoreoAplicaSuPropioFactor Then
                                _Producto_ME_MenudeoFactor = _Producto_ME_MayoreoFactor
                            End If
                        Else
                            If _Cliente_Precios_RangoAplicado = "Gran Mayorista" Then
                                If _Producto_GranMayoreo_Aplica Then
                                    _Producto_PrecioVendido = _Producto_GranMayoreo_Precio
                                End If
                                '
                                If _Producto_ME_GranMayAplicaSuPropioFactor Then
                                    _Producto_PrecioVendido = _Producto_ME_GranMayFactor
                                End If
                            Else
                                If _Cliente_Precios_RangoAplicado = "% por Definir" Then
                                    _Producto_PrecioVendido = _Producto_PrecioCosto * (1 + (_Cliente_Precios_PorcentADefinir / 100))
                                Else
                                    _Producto_PrecioVendido = 0
                                    MessageBox.Show("ERROR. El Cliente no tiene Asignado Rango de Precios. Consulte a su Soporte Tecnico.")
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            '
            If Len(Trim(_Producto_Foto)) = 0 Then
                btn_FotoProducto.BackgroundImage = cLibreriasImagen.StringToImage(gPersonalizacion("Gen_FotoProductoX").ToString())
                'btn_FotoProducto.Text = Per_PV_Leyenda
                _Producto_Foto = ""
                ProductoTieneImagen = False
            Else
                btn_FotoProducto.BackgroundImage = cLibreriasImagen.StringToImage(_Producto_Foto)
                'btn_FotoProducto.Text = ""
                '_Producto_Foto = _Producto_Foto
                ProductoTieneImagen = True
            End If
        Catch RED As Exception
            MessageBox.Show("fun_DatosProductoAVisualizarMain(). Error " & RED.Message.ToString())
        End Try
        Return vl_DatosOk
    End Function
    Private Function fun_ProductosCoincidenConLaDescripcion(ByRef p_ProductoDescripcion As String) As Double
        Dim ContadorCoincidencias As Double = 0
        '
        Try
            Dim conexion As New SqlConnection()
            conexion.ConnectionString = vg_ConexionBD_Principal
            Dim comando As New SqlCommand()
            comando.CommandText = "SELECT COUNT(*) AS Cuantos FROM Productos WHERE Inactivo = 0 AND Descripcion LIKE '%" & p_ProductoDescripcion & "%' OR " & _
                                  "      CodigoFabricante LIKE '%" & p_ProductoDescripcion & "%'"
            comando.Connection = conexion
            conexion.Open()
            Dim dr As SqlDataReader = comando.ExecuteReader()
            dr.Read()
            ContadorCoincidencias = Val(dr("Cuantos").ToString)
            dr.Close()
        Catch RED As Exception
            MessageBox.Show("fun_ProductosCoincidenConLaDescripcion(). Error " & RED.Message.ToString())
        End Try
        '
        Return ContadorCoincidencias
    End Function
    Private Function fun_ObtenerCodigoDelProducto(ByVal p_ProductoDescripcion) As Double
        Dim CodigoBarrasProducto As Double = 0
        Try
            Dim conexion As New SqlConnection()
            conexion.ConnectionString = vg_ConexionBD_Principal
            Dim comando As New SqlCommand()
            comando.CommandText = "SELECT CodigoBarras, Descripcion, PrecioCosto FROM Productos " & _
                                  "WHERE Inactivo = 0 AND Descripcion LIKE '%" & p_ProductoDescripcion & "%' OR " & _
                                  "      CodigoFabricante LIKE '%" & p_ProductoDescripcion & "%'"
            comando.Connection = conexion
            conexion.Open()
            Dim dr As SqlDataReader = comando.ExecuteReader()
            If dr.HasRows Then
                While dr.Read()
                    CodigoBarrasProducto = Val(dr("CodigoBarras").ToString)
                End While
            End If
            dr.Close()
            '
        Catch RED As Exception
            MessageBox.Show("fun_ObtenerCodigoDelProducto(). Error " & RED.Message.ToString())
        End Try
        Return CodigoBarrasProducto
    End Function
    Private Sub prog_ObtenerDatosClienteAVisualizar(ByVal p_CodigoCliente As Integer)
        Try
            Dim conexion As New SqlConnection()
            conexion.ConnectionString = vg_ConexionBD_Principal
            Dim comando As New SqlCommand()
            comando.CommandText = "SELECT *, ISNULL(FotoCliente, '') AS FotoCliente FROM Clientes WHERE Id = " & p_CodigoCliente.ToString()
            comando.Connection = conexion
            conexion.Open()
            Dim dr As SqlDataReader = comando.ExecuteReader()
            If dr.HasRows Then
                While dr.Read()
                    _Cliente_RazonSocial = dr("RazonSocial").ToString
                    _Cliente_Precios_ObligatorioTarjeta = dr("Precios_ObligatorioPresentarTarjeta").ToString
                    _Cliente_Precios_RangoAplicado = dr("Precios_RangoAplicado").ToString
                    _Cliente_Precios_PorcentADefinir = Val(dr("Precios_PorcentajeADefinir").ToString)
                    _Cliente_Precios_PreguntarSiImprimirMayMen = dr("Precios_PreguntarImprimirMayMen").ToString
                    _Cliente_ME_Aplica = dr("MonederoElectronico_Aplica").ToString()
                    _Cliente_ME_FactorPrincipal = dr("MonederoElectronico_FactorPrincipal").ToString()
                    _Cliente_ME_SiImporteMEExcedeUtilidadEn = dr("MonederoElectronico_SiImporteMEExcedeUtilidadEn").ToString()
                    _Cliente_ME_AplicaEsteFactorAlterno = dr("MonederoElectronico_AplicaEsteFactorAlterno").ToString()
                    _Cliente_ME_Disponible = dr("MonederoElectronico_Disponible").ToString()
                    _Cliente_ME_MinimoCambiar = dr("MonederoElectronico_MinimoCambiar").ToString
                    _Cliente_Credito_Aplica = dr("Credito_Aplica").ToString
                    _Cliente_Credito_Deshabilitado = dr("Credito_Deshabilitar").ToString
                    _Cliente_Credito_Dias = dr("Credito_DiasVencimiento").ToString
                    _Cliente_Credito_InteresMensual = dr("Credito_InteresMensual").ToString
                    _Cliente_Credito_Limite = dr("Credito_Limite").ToString
                    _Cliente_Credito_Usado = dr("Credito_Usado").ToString
                    _Cliente_Credito_Disponible = dr("Credito_Disponible").ToString()
                    _Cliente_Credito_BloquearSiDebe = dr("Credito_BloquearSiDebe").ToString()
                    _Cliente_Credito_MinimoCompra = dr("Credito_MinimoCompra").ToString
                    _Cliente_InternoAplica = dr("Interno_Aplica").ToString
                    _Cliente_InternoCuentaContableCargo = dr("Interno_CuentaContableCargo").ToString
                    _Cliente_InternoCuentaContableAbono = dr("Interno_CuentaContableAbono").ToString
                    _Cliente_FotoCliente = Trim(dr("FotoCliente").ToString)
                    _Cliente_FotoMasivaCliente = Trim(dr("FotoClienteMasivo").ToString)
                    _Cliente_Bloqueado = dr("Bloqueado").ToString
                End While
            End If
            dr.Close()
        Catch RED As Exception
            MessageBox.Show("prog_ObtenerDatosClienteAVisualizar(). Error " & RED.Message.ToString())
        End Try
        '
        _Cliente_Id = p_CodigoCliente
        labelClienteClave.Text = Format(_Cliente_Id, "0000")
        labelClienteNombre.Text = _Cliente_RazonSocial
        '
        If _Cliente_ME_Aplica And Per_MonederoElectronico_Aplica Then
            Me.labelMonederoElectronico.Visible = True
            Me.labelMonederoElectronico.Text = "Disponible ME: " & Format(Math.Round(_Cliente_ME_Disponible, 1), "#,##0.00")
        Else
            Me.labelMonederoElectronico.Visible = False
            Me.labelMonederoElectronico.Text = "Sin Monedero Electronico"
        End If
        '
        If Per_Credito_Aplica And _Cliente_Credito_Aplica And _Cliente_Credito_Usado > 0 Then
            Me.labelCreditoAdeudo.Visible = True
            Me.labelCreditoAdeudo.Text = "Adeudo: " & Format(Math.Round(_Cliente_Credito_Usado, 1), "#,##0.00")
            '
            Me.timerParpadeaEtiquetas.Start()
            Me.timerParpadeaEtiquetas.Enabled = True
        Else
            Me.labelCreditoAdeudo.Visible = False
            Me.labelCreditoAdeudo.Text = "No aplica"
            '
            Me.timerParpadeaEtiquetas.Stop()
            Me.timerParpadeaEtiquetas.Enabled = False
        End If
        '
        label_RangoPrecios.Text = _Cliente_Precios_RangoAplicado
        If _Cliente_Precios_RangoAplicado = "% por Definir" Then
            label_RangoPrecios.Text = label_RangoPrecios.Text & ": " & fun_FormatoCantidad(_Cliente_Precios_PorcentADefinir) & " %"
        End If
        '
        Try
            If _Cliente_FotoCliente = "" Then
                'btn_Clientes.BackgroundImage = Image.FromFile(gTerminalBD_Ruta_FotosClientes & "\Clientes.jpg")
                btn_Clientes.BackgroundImage = cLibreriasImagen.StringToImage(gPersonalizacion("Gen_FotoClienteProveedorX").ToString())

            Else
                'btn_Clientes.BackgroundImage = Image.FromFile(gTerminalBD_Ruta_FotosClientes & "\" & _Cliente_FotoLogo)
                btn_Clientes.BackgroundImage = cLibreriasImagen.StringToImage(_Cliente_FotoCliente)
            End If
        Catch
        End Try
        '
        label_TarjetaSiNo.Visible = False
    End Sub                            'prog_DatosClienteAVisualizar
    Private Function fun_GrabaVentasEn_tbVentasPVDetalle() As Boolean
        Dim GrabarOk As Boolean = False
        '
        'Dim MEAplica As Boolean = False
        Dim MEFactor As Decimal = 0
        Dim MEImporte As Decimal = 0
        Dim MEFactorProvinoDelProducto = False
        '
        If _Producto_ME_MenudeoAplicaSuPropioFactor Then
            MEFactor = _Producto_ME_MenudeoFactor
            MEImporte = (_Producto_Cantidad * _Producto_PrecioVendido) * (_Producto_ME_MenudeoFactor / 100)
            MEFactorProvinoDelProducto = True
        End If
        '
        'Si el producto no aplica para Monedero Electronico, resetear estas variables
        If Not _Producto_ME_Aplica Then
            MEFactor = 0
            MEImporte = 0
            MEFactorProvinoDelProducto = False
        End If
        '
        Try
            Dim VL_SqlConnection As New SqlConnection(vg_ConexionBD_Principal)
            Dim VL_ComandoSql As String = "INSERT INTO dbo.VentasPV_Detalle (" & _
                "TerminalId, " & _
                "NivelDetalle, " & _
                "ProductoId, " & _
                "DescripcionProducto, " & _
                "DescripcionAdicionalProducto, " & _
                "PUCosto, " & _
                "PUVenta, " & _
                "PUVendido, " & _
                "CantidadSurtida, " & _
                "MonederoElectronico_Aplica, " & _
                "MonederoElectronico_Factor, " & _
                "MonederoElectronico_Importe, " & _
                "MonederoElectronico_FactorProducto, " & _
                "DesconsolidarEn, " & _
                "RequiereDescripcion) Values (" & _
                Str(gTerminalXML_Id) & ", " & _
                "0, " & _
                Str(_Producto_Id) & ", '" & _
                Trim(_Producto_Descripcion) & "', '" & _
                Trim(_Producto_DescripcionAdicional) & "', " & _
                Str(_Producto_PrecioCosto) & ", " & _
                Str(_Producto_PrecioVentaMenudeo) & ", " & _
                Str(_Producto_PrecioVendido) & ", " & _
                Str(_Producto_Cantidad) & ", " & _
                CType(_Producto_ME_Aplica, Integer).ToString() & ", " & _
                MEFactor.ToString() & ", " & _
                MEImporte.ToString() & ", " & _
                CType(MEFactorProvinoDelProducto, Integer).ToString() & ", " & _
                Str(_Producto_DesconEn) & ", " & _
                Val(_Producto_RequiereDescripcion) & ")"
            Dim da_CatalogoProductos As New SqlDataAdapter(VL_ComandoSql, VL_SqlConnection)
            Dim ds_CatalogoProductos As New DataSet
            da_CatalogoProductos.Fill(ds_CatalogoProductos)

            GrabarOk = True
        Catch RED As Exception
            MessageBox.Show("fun_GrabaVentasEn_tbVentasPVDetalle(). Error: " & RED.Message.ToString())
        End Try
        Return GrabarOk
    End Function                'fun_GrabaVentasTempDetalle
    Private Sub RecargaGridVentas()
        Try
            'Dim ActualDireccionDeFila As Point = dgv_Ventas.CurrentCellAddress                         'guarda pocision de la fila actual dgv_Encabezado
            Dim dv_ As New DataView
            Dim VL_SqlConnection As New SqlConnection(vg_ConexionBD_Principal)
            Dim VL_ComandoSql As String = "SELECT CodigoBarras, DescripcionProducto, CantidadSurtida, " & _
                                  "PUVendido, CantidadSurtida * PUVendido AS Importe, " & _
                                  "CR, TerminalId, NivelDetalle, " & _
                                  "PUCosto, " & _
                                  "PUVenta, " & _
                                  "Productos.RequiereDescripcion,  " & _
                                  "MedioMayoreo_Aplica, MedioMayoreo_Utilidad, MedioMayoreo_PrecioVenta, " & _
                                  "MedioMayoreo_CantidadMinima, Mayoreo_Aplica, Mayoreo_Utilidad, " & _
                                  "Mayoreo_PrecioVenta, Mayoreo_CantidadMinima, GranMayoreo_Aplica, GranMayoreo_Utilidad, " & _
                                  "GranMayoreo_PrecioVenta, GranMayoreo_CantidadMinima, EmbalajeId, CantidadActual, " & _
                                  "Productos.RequiereDescripcion, Inactivo, ISNULL(FotoProducto,'') AS FotoProducto, " & _
                                  "DescripcionAdicionalProducto, DesconsolidarEn, ProductoId, FotoProductoMasivo " & _
                                  "FROM dbo.VentasPV_Detalle " & _
                                  "LEFT JOIN Productos ON ProductoId = ID " & _
                                  "WHERE NivelDetalle = 0 AND TerminalId = " & Str(gTerminalXML_Id) & " " & _
                                  "ORDER BY CR DESC"
            Dim da_ As New SqlDataAdapter(VL_ComandoSql, VL_SqlConnection)
            Dim ds_ As New DataSet
            da_.Fill(ds_)
            dv_.Table = ds_.Tables(0)
            dgv_Ventas.DataSource = dv_
            '
            Obtener_NumLineas_GranTotal_tbVentasPVDetalle()
            'dgv_Ventas.CurrentCell = dgv_Ventas.Rows(ActualDireccionDeFila.Y).Cells(ActualDireccionDeFila.X)  'Posiciono el cursor
            '
        Catch RED As Exception
            MessageBox.Show("RecargaGridVentas(). ERROR " & RED.Message.ToString())
        End Try
    End Sub
    Private Sub Obtener_NumLineas_GranTotal_tbVentasPVDetalle()
        Try
            Dim conexion As New SqlConnection()
            conexion.ConnectionString = vg_ConexionBD_Principal
            Dim comando As New SqlCommand()
            comando.CommandText = "SELECT * FROM VentasPV_Detalle WHERE NivelDetalle = 0 AND TerminalId = " & Str(gTerminalXML_Id)
            comando.Connection = conexion
            conexion.Open()
            Dim dr As SqlDataReader = comando.ExecuteReader()
            If dr.HasRows Then
                vf_GranTOTAL = 0
                _ImportePositivo_Venta = 0
                _ImporteNegativo_Venta = 0
                vf_ContadorItems = 0
                '
                While dr.Read()
                    Dim Importe As Decimal = Val(dr("CantidadSurtida").ToString()) * Val(dr("PUVendido").ToString())
                    vf_ContadorItems = vf_ContadorItems + 1
                    vf_GranTOTAL = vf_GranTOTAL + Importe
                    '
                    If Importe > 0 Then
                        _ImportePositivo_Venta = _ImportePositivo_Venta + Importe
                    End If
                    '
                    If Importe < 0 Then
                        _ImporteNegativo_Venta = _ImporteNegativo_Venta + Importe
                    End If
                End While
            Else
                vf_GranTOTAL = 0
                _ImportePositivo_Venta = 0
                _ImporteNegativo_Venta = 0
                vf_ContadorItems = 0
            End If
            dr.Close()
            Label_GranTotal.Text = Format(vf_GranTOTAL, "###,###,##0.00")
        Catch RED As Exception
            MessageBox.Show("Obtener_NumLineas_GranTotal_tbVentasPVDetalle(). Error " & RED.Message.ToString())
        End Try
    End Sub
    Private Sub prog_LimpiaTablaVentasTemporales()
        Dim DataSet_Programacion As New DataSet
        Try
            Dim VL_SqlConnection = New SqlConnection(vg_ConexionBD_Principal)

            Dim VL_ComandoSql = "DELETE FROM VentasPV_Detalle WHERE NivelDetalle = 0 AND TerminalId = " & Str(gTerminalXML_Id)
            Dim VL_DataAdapter = New SqlDataAdapter(VL_ComandoSql, VL_SqlConnection)

            VL_DataAdapter.Fill(DataSet_Programacion)

        Catch RED As Exception
            MessageBox.Show("prog_LimpiaTablaVentasTemporales. Error:" & RED.Message.ToString())
        End Try
    End Sub
    Private Sub prog_RegistraEnBitacoraVentasCanceladas()
        Try
            Dim VL_SqlConnection As New SqlConnection(vg_ConexionBD_Principal)
            Dim VL_ComandoSql As String = "INSERT INTO Ventas_Canceladas (" & _
                                          "     Fecha, " & _
                                          "     ProductoId, " & _
                                          "     Cantidad, " & _
                                          "     Precio, " & _
                                          "     Importe, " & _
                                          "     MotivosEliminacion, " & _
                                          "     UsuarioId) " & _
                                          "SELECT '" & _
                                                Now & "', " & _
                                          "     ProductoId, " & _
                                          "     CantidadSurtida, " & _
                                          "     PUVendido, " & _
                                          "     PUVendido * CantidadSurtida, " & _
                                          "     SubString(DescripcionProducto + '. ' + '" & Mid(Trim(vf_VentaCancelada_Justificacion), 1, 50) & "',1,70), " & _
                                                Str(gUsuario_Id) & " " & _
                                          "FROM VentasPV_Detalle " & _
                                          "WHERE NivelDetalle = 0 AND TerminalId = " & Str(gTerminalXML_Id)
            Dim da_ As New SqlDataAdapter(VL_ComandoSql, VL_SqlConnection)
            Dim ds_ As New DataSet
            da_.Fill(ds_)
        Catch RED As Exception
            MessageBox.Show("prog_RegistraEnBitacoraVentasCanceladas(). Error: " & RED.Message.ToString())
        End Try
    End Sub
    Private Sub prog_RegistraEnBitacora_RegistroEliminado()
        Try
            Dim VL_SqlConnection As New SqlConnection(vg_ConexionBD_Principal)
            Dim VL_ComandoSql As String = "INSERT INTO Ventas_Canceladas (" & _
                                          "     Fecha, " & _
                                          "     ProductoId, " & _
                                          "     Cantidad, " & _
                                          "     Precio, " & _
                                          "     Importe, " & _
                                          "     MotivosEliminacion, " & _
                                          "     UsuarioId) " & _
                                          "SELECT '" & _
                                                Now & "', " & _
                                          "     ProductoId, " & _
                                          "     CantidadSurtida, " & _
                                          "     PUVendido, " & _
                                          "     PUVendido * CantidadSurtida, " & _
                                          "     DescripcionProducto + '. ' + '" & Mid(vf_VentaCancelada_Justificacion, 1, 50) & "', " & _
                                                Str(gUsuario_Id) & " " & _
                                          "FROM VentasPV_Detalle " & _
                                          "WHERE CR = " & Trim(dgv_Ventas.SelectedCells(8).Value.ToString())
            Dim da_ As New SqlDataAdapter(VL_ComandoSql, VL_SqlConnection)
            Dim ds_ As New DataSet
            da_.Fill(ds_)
        Catch RED As Exception
            MessageBox.Show("prog_RegistraEnBitacora_RegistroEliminado(). Error: " & RED.Message.ToString())
        End Try
    End Sub
    Private Function PasaVentaTemporalAVentas_Encabezado() As Boolean
        '
        vf_Ventas_FolioAGrabar = fun_UltimoFolio_Venta()
        '
        If cLibreriasBD.InsertUpdate("INSERT INTO Ventas (" & _
                                          "FolioVenta, " & _
                                          "Fecha, " & _
                                          "ClienteId, " & _
                                          "ImporteVenta, " & _
                                          "ImporteEfectivoEntregado, " & _
                                          "ImportePago_ME, " & _
                                          "ImportePago_Credito, " & _
                                          "ImportePago_OtroMedio, " & _
                                          "ImportePago_Referencia, " & _
                                          "ImportePago_Total, " & _
                                          "ImporteVuelto, " & _
                                          "ImportePagoAdeudo, " & _
                                          "NumeroTarjeta, " & _
                                          "MonederoElectronico_Importe, " & _
                                          "MonederoElectronico_Aplicado, " & _
                                          "ConsumoInterno, " & _
                                          "VentaCancelada, " & _
                                          "TerminalId, " & _
                                          "UsuarioId) Values (" & _
                                          Str(vf_Ventas_FolioAGrabar + 1) & ", '" & _
                                          Now & "', " & _
                                          Str(_Cliente_Id) & ", " & _
                                          Str(_ImportePositivo_Venta - _ImporteNegativo_Venta) & ", " & _
                                          Str(_ImportePago_Efectivo) & ", " & _
                                          Str(_ImportePago_ME) & ", " & _
                                          Str(_ImportePago_Credito) & ", " & _
                                          "0, '" & _
                                          _ImportePago_Referencia & "', " & _
                                          Str(_ImportePago_TOTAL) & ", " & _
                                          Str(_Importe_Vuelto) & ", " & _
                                          Str(_Importe_AdeudoAPagar) & ", '" & _
                                          _Tarjeta_Numero & "', " & _
                                          Str(cTarjeta.ME_ImportexEstaVenta) & ", 0, " & _
                                          CType(_Cliente_InternoAplica, Byte) & ", " & _
                                          "0, " & _
                                          Str(gTerminalXML_Id) & ", " & _
                                          Str(gUsuario_Id) & ")") Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function CreaIngresoXVenta() As Boolean
        '
        If cLibreriasBD.InsertUpdate("INSERT INTO Ingresos (" & _
                                          "Ref_FolioVenta, " & _
                                          "Fecha, " & _
                                          "ClienteId, " & _
                                          "Ref_ImporteEfectivoEntregado, " & _
                                          "Ref_ImportePago_ME, " & _
                                          "Ref_ImportePago_Credito, " & _
                                          "Ref_ImportePago_OtroMedio, " & _
                                          "Ref_ImportePago_Referencia, " & _
                                          "Ref_ImportePago_Total, " & _
                                          "Ref_ImporteVuelto, " & _
                                          "Importe_Ingreso, " & _
                                          "Aplicado, UsuarioId, Cancelado, CanceladoUsuarioId, CanceladoFecha) Values (" & _
                                          Str(vf_Ventas_FolioAGrabar + 1) & ", " & _
                                          "GETDATE(), " & _
                                          Str(_Cliente_Id) & ", " & _
                                          Str(_ImportePago_Efectivo) & ", " & _
                                          Str(_ImportePago_ME) & ", " & _
                                          Str(_ImportePago_Credito) & ", " & _
                                          "0, '" & _
                                          _ImportePago_Referencia & "', " & _
                                          Str(_ImportePago_TOTAL) & ", " & _
                                          Str(_Importe_Vuelto) & ", " & _
                                          Str(_ImportePositivo_Venta - _ImporteNegativo_Venta - (_ImportePago_ME + _ImportePago_Credito)) & ", " & _
                                          " 1, " & Str(gUsuario_Id) & ", 0, 0, '2001-01-01 00:00:00')") Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub CreaIngresoXPagoDeAdeudo()
        '
        If cLibreriasBD.InsertUpdate("INSERT INTO Ingresos (" & _
                                          "Ref_FolioVenta, " & _
                                          "Fecha, " & _
                                          "ClienteId, " & _
                                          "Ref_ImporteEfectivoEntregado, " & _
                                          "Ref_ImportePago_ME, " & _
                                          "Ref_ImportePago_Credito, " & _
                                          "Ref_ImportePago_OtroMedio, " & _
                                          "Ref_ImportePago_Referencia, " & _
                                          "Ref_ImportePago_Total, " & _
                                          "Ref_ImporteVuelto, " & _
                                          "Importe_Ingreso, " & _
                                          "Aplicado, UsuarioId, Cancelado, CanceladoUsuarioId, CanceladoFecha) Values (" & _
                                          Str(vf_Ventas_FolioAGrabar + 1) & ", " & _
                                          "GETDATE(), " & _
                                          Str(_Cliente_Id) & ", " & _
                                          Str(_ImportePago_Efectivo) & ", " & _
                                          Str(_ImportePago_ME) & ", " & _
                                          Str(_ImportePago_Credito) & ", " & _
                                          "0, 'Pago de Adeudo', " & _
                                          Str(_ImportePago_TOTAL) & ", " & _
                                          Str(_Importe_Vuelto) & ", " & _
                                          Str(_Importe_AdeudoAPagar) & ", " & _
                                          " 1, " & Str(gUsuario_Id) & ", 0, 0, '2001-01-01 00:00:00')") Then
            'Return True
        Else
            'Return False
        End If
    End Sub
    Private Function prog_PasaVentaTemporalAVentas() As Boolean
        Dim Ok As Boolean = False
        '
        Try
            Dim VL_SqlConnection As New SqlConnection(vg_ConexionBD_Principal)
            Dim VL_ComandoSql As String = "INSERT INTO Ventas_Detalle (" & _
                                          "   FolioVenta, " & _
                                          "   ProductoId, " & _
                                          "   DescripcionProducto, " & _
                                          "   DescripcionAdicionalProducto, " & _
                                          "   PUCosto, " & _
                                          "   PUVenta, " & _
                                          "   PUVendido, " & _
                                          "   CantidadSurtida, " & _
                                          "   MonederoElectronico_Aplica, " & _
                                          "   MonederoElectronico_Factor, " & _
                                          "   MonederoElectronico_Importe, " & _
                                          "   DesconsolidarEn) " & _
                                          "SELECT " & _
                                              Str(vf_Ventas_FolioAGrabar + 1) & ", " & _
                                          "   ProductoId, " & _
                                          "   DescripcionProducto, " & _
                                          "   DescripcionAdicionalProducto, " & _
                                          "   PUCosto * DesconsolidarEn, " & _
                                          "   PUVenta * DesconsolidarEn , " & _
                                          "   PUVendido * DesconsolidarEn , " & _
                                          "   CantidadSurtida / DesconsolidarEn, " & _
                                          "   MonederoElectronico_Aplica, " & _
                                          "   MonederoElectronico_Factor, " & _
                                          "   MonederoElectronico_Importe, " & _
                                          "   DesconsolidarEn " & _
                                          "FROM VentasPV_Detalle " & _
                                          "WHERE NivelDetalle = 0 AND TerminalId = " & Str(gTerminalXML_Id) & _
                                          "  " & _
                                          "; " & _
                                          "  " & _
                                          "UPDATE Productos SET " & _
                                          "  CantidadTotalVendida = CantidadTotalVendida + i.Cantidad, " & _
                                          "  CantidadActual = CantidadActual - i.Cantidad, " & _
                                          "  Inactivo = 0 " & _
                                          "FROM " & _
                                          "     (SELECT ProductoId, sum(CantidadSurtida) as cantidad " & _
                                          "      FROM   Ventas_Detalle " & _
                                          "      WHERE  FolioVenta = " & Str(vf_Ventas_FolioAGrabar + 1) & _
                                          "      GROUP BY ProductoId)i " & _
                                          "WHERE Id = i.ProductoId" & _
                                          "  " & _
                                          "; " & _
                                          "  " & _
                                          "INSERT INTO Productos_Transacciones (" & _
                                          "   Referencia, " & _
                                          "   Fecha, " & _
                                          "   DetalleCR, " & _
                                          "   ProductoId, " & _
                                          "   CantidadAplicada, " & _
                                          "   CantidadAntes, " & _
                                          "   CantidadDespues) " & _
                                          "SELECT " & _
                                          "   'Venta', '" & _
                                          Now & "', " & _
                                          "   CR, " & _
                                          "   ProductoId, " & _
                                          "  -1*(CantidadSurtida), " & _
                                          "   CantidadActual + CantidadSurtida, " & _
                                          "   CantidadActual " & _
                                          "FROM Ventas_Detalle " & _
                                          "LEFT JOIN Productos ON Id = ProductoId " & _
                                          "WHERE FolioVenta = " & Str(vf_Ventas_FolioAGrabar + 1)
            Dim da_ As New SqlDataAdapter(VL_ComandoSql, VL_SqlConnection)
            Dim ds_ As New DataSet
            da_.Fill(ds_)
            Ok = True
        Catch RED As Exception
            MessageBox.Show("prog_PasaVentaTemporalAVentas(). Error: " & RED.Message.ToString())
        End Try
        Return Ok
    End Function
    Private Function fun_UltimoFolio_Venta() As Double
        Dim vl_UltimoFolioVenta As Double = 0
        Try
            Dim conexion As New SqlConnection()
            conexion.ConnectionString = vg_ConexionBD_Principal
            Dim comando As New SqlCommand()
            comando.CommandText = "SELECT TOP 1 * FROM Ventas ORDER BY FolioVenta DESC"
            comando.Connection = conexion
            conexion.Open()
            Dim dr As SqlDataReader = comando.ExecuteReader()
            If dr.HasRows Then
                While dr.Read()
                    vl_UltimoFolioVenta = dr("FolioVenta").ToString()
                End While
            End If
            dr.Close()
        Catch RED As Exception
            MessageBox.Show("fun_UltimoFolioVenta(). Error " & RED.Message.ToString())
        End Try
        Return vl_UltimoFolioVenta
    End Function
    Private Sub prog_PasaVentaTemporalA_MovAlmacenDetalle(ByVal p_ClaveAlm As String, ByVal p_Referencia As String, _
                                                           ByVal p_Prefijo As String, ByVal parametro_AdicionaResta As String)
        Dim signo1 As String = "-"
        Dim signo2 As String = "+"
        '
        If parametro_AdicionaResta = "Adiciona" Then
            signo1 = "+"
            signo2 = "-"
        End If

        vf_MovAlmacen_FolioAGrabar = fun_UltimoFolio_MovAlmacen()
        Try
            Dim VL_SqlConnection As New SqlConnection(vg_ConexionBD_Principal)
            Dim VL_ComandoSql As String = "INSERT INTO MovimientosAlmacen (" & _
                                          "FolioMovimiento, " & _
                                          "Fecha, " & _
                                          "MovAlmacenId, " & _
                                          "EntradaSalida, " & _
                                          "AlmacenId, " & _
                                          "Referencia, " & _
                                          "Importe, " & _
                                          "UsuarioId, " & _
                                          "AfectoInventario, " & _
                                          "Bloqueado) Values (" & _
                                           Str(vf_MovAlmacen_FolioAGrabar + 1) & ", '" & _
                                           Now & "', " & _
                                           p_ClaveAlm & ", " & _
                                          "'S', 1, '" & _
                                           p_Referencia & "', " & _
                                           vf_Total_Costo_MovAlmacen & ", " & _
                                           Str(gUsuario_Id) & ",1 , 0)" & _
                                          "  " & _
                                          "; " & _
                                          "  " & _
                                          "INSERT INTO MovimientosAlmacen_Detalle (" & _
                                          "     FolioMovimiento, " & _
                                          "     ProductoId, " & _
                                          "     DescripcionProducto, " & _
                                          "     DescripcionAdicionalProducto, " & _
                                          "     Cantidad, " & _
                                          "     PUCosto, " & _
                                          "     PUVenta, " & _
                                          "     ImporteCosto, " & _
                                          "     ImporteVenta, " & _
                                          "     AfectoInventario, " & _
                                          "     Bloqueado) " & _
                                          "SELECT " & _
                                                Str(vf_MovAlmacen_FolioAGrabar + 1) & ", " & _
                                          "     ProductoId, " & _
                                          "     DescripcionProducto, " & _
                                          "     DescripcionAdicionalProducto, " & _
                                          "     CantidadSurtida, " & _
                                          "     PUCosto, " & _
                                          "     PUVenta, " & _
                                          "     PUCosto * CantidadSurtida, " & _
                                          "     PUVenta * CantidadSurtida, " & _
                                          "     1, 0 " & _
                                          "FROM VentasPV_Detalle " & _
                                          "WHERE NivelDetalle = 0 AND TerminalId = " & Str(gTerminalXML_Id) & _
                                          "  " & _
                                          "; " & _
                                          "  " & _
                                           "UPDATE Productos SET " & _
                                          "  CantidadTotalMovInt = CantidadTotalMovInt " & signo2 & " i.Cantidad, " & _
                                          "  CantidadActual = CantidadActual " & signo1 & " i.Cantidad, " & _
                                          "  Inactivo = 0 " & _
                                          "FROM " & _
                                          "     (SELECT ProductoId, sum(Cantidad) as Cantidad " & _
                                          "      FROM   MovimientosAlmacen_Detalle " & _
                                          "      WHERE  FolioMovimiento = " & Str(vf_MovAlmacen_FolioAGrabar + 1) & " AND " & _
                                          "             AfectoInventario = 1 " & _
                                          "      GROUP BY ProductoId)i " & _
                                          "WHERE Id = i.ProductoId" & _
                                          "  " & _
                                          "; " & _
                                          "  " & _
                                          "INSERT INTO Productos_Transacciones (" & _
                                          "   Referencia, " & _
                                          "   Fecha, " & _
                                          "   DetalleCR, " & _
                                          "   ProductoId, " & _
                                          "   CantidadAplicada, " & _
                                          "   CantidadAntes, " & _
                                          "   CantidadDespues) " & _
                                          "SELECT " & _
                                          "'" & Mid("MovAlmacen-" & Trim(p_Prefijo) & ". Ref.: " & Trim(p_Referencia), 1, 100) & "', " & _
                                          "'" & Now & "', " & _
                                          "   CR, " & _
                                          "   ProductoId, " & _
                                          signo1 & "1*(Cantidad), " & _
                                          "   CantidadActual " & signo2 & "Cantidad, " & _
                                          "   CantidadActual " & _
                                          "FROM MovimientosAlmacen_Detalle " & _
                                          "LEFT JOIN Productos ON Id = ProductoId " & _
                                          "WHERE FolioMovimiento = " & Str(vf_MovAlmacen_FolioAGrabar + 1)
            Dim da_ As New SqlDataAdapter(VL_ComandoSql, VL_SqlConnection)
            Dim ds_ As New DataSet
            da_.Fill(ds_)
        Catch RED As Exception
            MessageBox.Show("prog_PasaVentaTemporalA_MovAlmacenDetalle(). Error: " & RED.Message.ToString())
        End Try
    End Sub
    Private Function fun_UltimoFolio_MovAlmacen() As Double
        Dim vl_UltimoFolio_ As Double = 0
        Try
            Dim conexion As New SqlConnection()
            conexion.ConnectionString = vg_ConexionBD_Principal
            Dim comando As New SqlCommand()
            comando.CommandText = "SELECT TOP 1 * FROM MovimientosAlmacen ORDER BY FolioMovimiento DESC"
            comando.Connection = conexion
            conexion.Open()
            Dim dr As SqlDataReader = comando.ExecuteReader()
            If dr.HasRows Then
                While dr.Read()
                    vl_UltimoFolio_ = Val(dr("FolioMovimiento").ToString())
                End While
            End If
            dr.Close()
        Catch RED As Exception
            MessageBox.Show("fun_UltimoFolio_MovAlmacen(). Error " & RED.Message.ToString())
        End Try
        Return vl_UltimoFolio_
    End Function

#Region "__________ Control PanelTrabajoDerecho"
    Private Sub PanelTrabajoDerecho_DeshabilitarControles()
        If _NumControlesEnPanelTrabajoDerecho = Me.panelTrabajoDerecho.Controls.Count() Then
            Dim x As Integer = 0
            While (x <= (_NumControlesEnPanelTrabajoDerecho - 1))
                Me.panelTrabajoDerecho.Controls.Item(x).Enabled = False
                x = x + 1
            End While
        End If
    End Sub
    Private Sub PanelTrabajoDerecho_HabilitarControles()
        Dim x As Integer = 0
        While (x <= (_NumControlesEnPanelTrabajoDerecho - 1))
            Me.panelTrabajoDerecho.Controls.Item(x).Enabled = True
            x = x + 1
        End While
        Me.txtLeerCodigo.Focus()
    End Sub

#End Region

#End Region

#Region "Botones Click"
    Private Sub lnk_NombreComercial_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnk_NombreComercial.LinkClicked
        form_DatosDelComercio.ShowDialog()
        picLogo.BackgroundImage = cLibreriasImagen.StringToImage(gPersonalizacion("Gen_LogoNombreArchivo").ToString())
        'picLogo.Image = cLibreriasImagen.StringToImage(gPersonalizacion("Gen_LogoNombreArchivo").ToString())
        Application.DoEvents()
    End Sub
    Private Sub btn_GuardarRecuperarVenta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_GuardarRecuperarVenta.Click
        If fun_ExisteUnaVentaGuardada() Then
            prog_CambiaA_0_ElFolioDeVentaTemporal()
            prog_InicializaVariablesXProducto()
            prog_InicializaVariablesXVenta()
            cLibreriasBD.InsertUpdate("DELETE FROM VentasPV_Detalle WHERE ProductoId = 0 OR CantidadSurtida = 0 OR PUVenta = 0 OR PUVendido = 0")
            'prog_EliminaVaciosDeTemp()
            RecargaGridVentas()
        Else
            prog_CambiaA_1_ElFolioDeVentaTemporal()
            prog_InicializaVariablesXProducto()
            prog_InicializaVariablesXVenta()
            RecargaGridVentas()
        End If
    End Sub
    Private Sub btn_Clientes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Clientes.Click
        If gUsuario_PV_Clientes_PuedeSeleccionar Then
            PanelTrabajoDerecho_DeshabilitarControles()
            '
            cCatalogoClientes = New CatalogoClientes()
            Me.panelTrabajoDerecho.Controls.Add(cCatalogoClientes)
            AddHandler cCatalogoClientes.ClickBotonesTerminar, AddressOf CatalogoClientes_Cerrar
            AddHandler cCatalogoClientes.ClickBotonPendientes, AddressOf ClientePendientes_Abrir
            cCatalogoClientes.Dock = DockStyle.Fill
            cCatalogoClientes.BringToFront()
        Else
            cLibreriasMensaje.Mostrar("El usuario no tiene permisos para acceder el Catalogo de Clientes", Mensajes.Emergente.Advertencia)
        End If
    End Sub
    Private Sub InicializaDatosTarjeta()
        _Tarjeta_Existe = False
        _Tarjeta_ClienteId = 0
        _Tarjeta_ME_Activada = False
        _Tarjeta_ME_FactorPrincipal = 0
        _Tarjeta_ME_SiImporteMEExcedeUtilidadEn = 0
        _Tarjeta_ME_AplicaEsteFactorAlterno = 0
        _Tarjeta_ME_Disponible = 0
        _Tarjeta_CreditoActivada = False
        _Tarjeta_CreditoLimite = 0
        _Tarjeta_CreditoUsado = 0
        _Tarjeta_CreditoDisponible = 0
        _Tarjeta_Cancelada = False
        '
        _Tarjeta_Numero = "000000"
        _Tarjeta_Id = 0
    End Sub
    Private Sub btn_Foto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_FotoProducto.Click
        Try
            Dim pivote As New form_ImagenAmpliada(Trim(dgv_Ventas.SelectedCells(IdxFotoMasivo).Value.ToString()), Trim(dgv_Ventas.SelectedCells(IdxCodigoBarras).Value.ToString()) & " - " & Trim(dgv_Ventas.SelectedCells(IdxDescripcionProducto).Value.ToString()))
            If pivote.ShowDialog() = DialogResult.OK Then
                '
            End If
        Catch
        End Try
    End Sub
    Private Sub btn_Productos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Productos.Click
        If vf_ContadorEnter = 0 Then
            'Envia los "enter" simulando que el usuario oprimio 2 veces enter
            My.Computer.Keyboard.SendKeys("{ENTER}", True)
            My.Computer.Keyboard.SendKeys("{ENTER}", True)
        Else
            My.Computer.Keyboard.SendKeys("{ENTER}", True)
        End If
    End Sub
    Private Sub btn_Cobrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Cobrar.Click
        txtLeerCodigo.Focus()
        My.Computer.Keyboard.SendKeys("{TAB}", True)
        My.Computer.Keyboard.SendKeys("{+}", True)
    End Sub
    Private Sub btn_MovAlmacen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_MovAlmacen.Click
        If vf_ContadorItems > 0 Then
            '
            Compacta_RegistrosDelVisorDeVentas()
            RecargaGridVentas()
            '
            Dim pivote As New form_RegistroDeOperacionEnBitacora("Ingresará al Modulo de Movimientos de Almacen", "Movimientos de Almacen", "Ingreso al Modulo")
            If pivote.ShowDialog = DialogResult.OK Then
                Dim piv As New form_PuntoDeVenta_MovAlmacen(fun_FormatoCantidad(Str(vf_Total_Costo_MovAlmacen)))
                If piv.ShowDialog = DialogResult.OK Then
                    cLibreriasBD.InsertUpdate("DELETE FROM VentasPV_Detalle WHERE ProductoId = 0 OR CantidadSurtida = 0 OR PUVenta = 0 OR PUVendido = 0")
                    'prog_EliminaVaciosDeTemp()
                    '
                    prog_PasaVentaTemporalA_MovAlmacenDetalle(piv.vp_ClaveMovAlmacen, piv.vp_Referencia, piv.vp_PrefijoMovAlmacen, piv.vp_AdicionaResta)
                    '
                    prog_LimpiaTablaVentasTemporales()
                    prog_InicializaVariablesXProducto()
                    prog_InicializaVariablesXVenta()
                    '
                    btn_FotoProducto.BackgroundImage = cLibreriasImagen.StringToImage(gPersonalizacion("Gen_FotoProductoX").ToString())
                    'btn_FotoProducto.Text = Per_PV_Leyenda
                    _Producto_Foto = ""
                    RecargaGridVentas()
                    '
                End If
            End If
        Else
            cLibreriasMensaje.Mostrar("No hay Productos para procesar", Mensajes.Emergente.Advertencia)
        End If
    End Sub
    Private Sub btn_CancelarVenta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_CancelarVenta.Click
        If gUsuario_PV_HabilitarBotonCancelar Then
            If gUsuario_PV_CancelarSinFirmar Then
                vf_VentaCancelada_Justificacion = "No se le requirio Justificacion"
                prog_RegistraEnBitacoraVentasCanceladas()
                prog_LimpiaTablaVentasTemporales()
                prog_InicializaVariablesXProducto()
                prog_InicializaVariablesXVenta()
                RecargaGridVentas()
            Else
                Dim pivote As New form_VentasCanceladas_Justificacion()
                If pivote.ShowDialog = DialogResult.OK Then
                    vf_VentaCancelada_Justificacion = pivote.vf_Descripcion
                    prog_RegistraEnBitacoraVentasCanceladas()
                    prog_LimpiaTablaVentasTemporales()
                    prog_InicializaVariablesXProducto()
                    prog_InicializaVariablesXVenta()
                    RecargaGridVentas()
                End If
            End If
        Else
            MessageBox.Show("NO TIENE PERMISOS PARA CANCELAR LA VENTA. " & Chr(13) & _
                            "Para devolver el Producto teclee el signo negativo y el codigo del Producto." & Chr(13) & Chr(13) & _
                            "Ejemplo 1:   -59 ( -  = Cantidad a devolver: 1, 59 = Codigo del Producto)" & Chr(13) & _
                            "Ejemplo 2: -3*59 ( -3 = Cantidad a devolver: 3, 59 = Codigo del Producto")
        End If
    End Sub
#End Region

#Region "Botones GotFocus"
    Private Sub lnk_NombreComercial_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_NombreComercial.GotFocus
        txtLeerCodigo.Focus()
    End Sub
    Private Sub btn_Productos_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Productos.GotFocus
        txtLeerCodigo.Focus()
    End Sub
    Private Sub btn_Cobrar_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cobrar.GotFocus
        txtLeerCodigo.Focus()
    End Sub
    Private Sub btn_Clientes_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Clientes.GotFocus
        txtLeerCodigo.Focus()
    End Sub
    Private Sub btn_MovAlmacen_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_MovAlmacen.GotFocus
        txtLeerCodigo.Focus()
    End Sub
    Private Sub btn_VentasAnteriores_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtLeerCodigo.Focus()
    End Sub
    Private Sub btn_GuardarRecuperarVenta_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_GuardarRecuperarVenta.GotFocus
        txtLeerCodigo.Focus()
    End Sub
    Private Sub btn_CancelarVenta_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_CancelarVenta.GotFocus
        txtLeerCodigo.Focus()
    End Sub
    Private Sub btn_NuevoProducto_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtLeerCodigo.Focus()
    End Sub
    Private Sub btn_Foto_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_FotoProducto.GotFocus
        txtLeerCodigo.Focus()
    End Sub
    Private Sub lnk_EsUnDemo_1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_EsUnDemo_1.GotFocus
        txtLeerCodigo.Focus()
    End Sub
    Private Sub lnk_EsUnDemo_2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_EsUnDemo_2.GotFocus
        txtLeerCodigo.Focus()
    End Sub
#End Region

#Region "ToolStripPrincipal"
    Private Sub ToolStripPrincipal_Regresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripPrincipal_Regresar.Click
        If Me.panelTrabajo.Controls.Count() > 1 Then
            Me.panelTrabajo.Controls.RemoveAt(0)
            If Me.panelTrabajoDerecho.Controls.Count = _NumControlesEnPanelTrabajoDerecho Then
                PanelTrabajoDerecho_HabilitarControles()
            End If
        Else
            RemoverControles_PanelTrabajoDerecho()
        End If
    End Sub
    Private Sub ToolStripPrincipal_Avanzar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripPrincipal_Avanzar.Click

    End Sub
    Private Sub ToolStripPrincipal_Notas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripPrincipal_Notas.Click
        form_Notas.ShowDialog()
    End Sub
    Private Sub ToolStripPrincipal_AcercaDe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripPrincipal_AcercaDe.Click
        ToolStripPrincipal.Visible = False
        StatusStripPrincipal.Visible = False
        lnk_NombreComercial.Enabled = False
        Me.panelTrabajo.Controls.Add(_ctrAcercaDe)
        'Me.Controls.Add(_ctrAcercaDe)
        PanelTrabajoDerecho_DeshabilitarControles()
        'Me.panelContenido.Controls.Add(_ctrAcercaDe)
        _ctrAcercaDe.Dock = DockStyle.Fill
        _ctrAcercaDe.BringToFront()
    End Sub
    Private Sub ToolStripPrincipal_ProductosNuevos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripPrincipal_ProductosNuevos.Click
        If gUsuario_Admin_Productos Then
            Dim pivote2 As New Productos_NuevoEditar("NUEVO", 0)
            If pivote2.ShowDialog = DialogResult.OK Then
                '
            End If
        Else
            cLibreriasMensaje.Mostrar("El Usuario no tiene Permisos para Crear Nuevos Usuarios", Mensajes.Emergente.Advertencia)
        End If
    End Sub
    Private Sub ToolStripPrincipal_VentaDelDia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripPrincipal_VentaDelDia.Click
        Dim pivote As New form_Ventas(gConfigSistema_FechaActual, 0, 0, gTerminalXML_Id)
        pivote.Show()
    End Sub
    Private Sub ToolStripPrincipal_CerrarSesion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripPrincipal_CerrarSesion.Click
        If MsgBox("Esta seguro que desea cerrar la Sesion ?", MsgBoxStyle.DefaultButton1 Or MsgBoxStyle.Information Or MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
            gEntorno_Reiniciar = True
            Application.Restart()
            SplashScreen.Show()
        End If
    End Sub
    Private Sub ToolStripPrincipal_Salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripPrincipal_Salir.Click
        Dim msg As String
        Dim title As String
        Dim style As MsgBoxStyle
        Dim response As MsgBoxResult
        msg = "Desea Salir del Programa SuperVentas ?"   ' Define message.
        style = MsgBoxStyle.DefaultButton2 Or _
           MsgBoxStyle.Information Or MsgBoxStyle.YesNo
        title = ""   ' Define title.
        ' Display message.
        response = MsgBox(msg, style, title)
        If response = MsgBoxResult.Yes Then   ' User chose Yes.
            While Me.Opacity > 0.05
                Me.Opacity = Me.Opacity - 0.075
            End While
            End
        End If
    End Sub
#End Region

#Region "Raise Event"
    Private Sub PanelControl_EventFlujoEfectivoAbrir()
        '_ctrFlujoEfectivo = New FlujoDeEfectivo
        PanelTrabajoDerecho_DeshabilitarControles()
        '_ctrFlujoEfectivo.prog_RestauraDatosFlujoEfectivo()
        Me.panelTrabajoDerecho.Controls.Add(_ctrFlujoEfectivo)
        prog_ActualizaAdministrador(gConfigSistema_FechaActual)
        _ctrFlujoEfectivo.prog_RestauraDatosFlujoEfectivo()
        _ctrFlujoEfectivo.Dock = DockStyle.Fill
        _ctrFlujoEfectivo.BringToFront()
    End Sub
    Private Sub PanelControl_EventCreditosAbrir()
        PanelTrabajoDerecho_DeshabilitarControles()
        Me.panelTrabajoDerecho.Controls.Add(_ctrCreditos)
        _ctrCreditos.Dock = DockStyle.Fill
        _ctrCreditos.BringToFront()
    End Sub
    Private Sub PanelControl_EventCatalogosAbrir()
        PanelTrabajoDerecho_DeshabilitarControles()
        Me.panelTrabajoDerecho.Controls.Add(_ctrCatalogos)
        _ctrCatalogos.Dock = DockStyle.Fill
        _ctrCatalogos.BringToFront()
    End Sub
    Private Sub PanelControl_EventTarjetasAbrir()
        PanelTrabajoDerecho_DeshabilitarControles()
        Me.panelTrabajoDerecho.Controls.Add(_ctrAdministradorDeTarjetas)
        _ctrAdministradorDeTarjetas.Dock = DockStyle.Fill
        _ctrAdministradorDeTarjetas.BringToFront()
    End Sub
    '
    'Remover Controles
    Private Sub Cobrando_EventCerrar()
        Dim _CobradoOk As Boolean
        '
        'Dim x As System.Windows.Forms.DialogResult = piv.ShowDialog()
        _ImportePago_Efectivo = cLibreriasDatos.ValorDecimal(_ctrCobrando.txtCobrandoEfectivoRecibido.Text)
        _ImportePago_ME = cLibreriasDatos.ValorDecimal(_ctrCobrando.txtCobrandoME.Text)
        _ImportePago_Credito = cLibreriasDatos.ValorDecimal(_ctrCobrando.txtCobrandoCredito.Text)
        '_ImportePago_OtroMedio = cLibreriasDatos.ValorDecimal(_ctrCobrando.txtCobrandoOtros.Text)
        _ImportePago_Referencia = ""
        _ImportePago_TOTAL = cLibreriasDatos.ValorDecimal(_ctrCobrando.boxImportePagoCliente_TOTAL.Text)
        _Importe_AdeudoAPagar = cLibreriasDatos.ValorDecimal(_ctrCobrando.txtAdeudoAPagar.Text)
        _Importe_Vuelto = cLibreriasDatos.ValorDecimal(_ctrCobrando.boxVuelto.Text)
        '_Cliente_Credito_Usado
        _CobradoOk = _ctrCobrando._CobradoOk
        _ImporteYaSeCapturoElPagoDelCliente = _ctrCobrando.labelEfectivoRecibido.Visible
        '
        RemoverControles_PanelTrabajoDerecho()
        '
        If _CobradoOk Then
            'prog_EliminaVaciosDeTemp()
            '
            'ELIMINA REGISTROS VACIOS O NULOS
            cLibreriasBD.InsertUpdate("DELETE FROM VentasPV_Detalle WHERE ProductoId = 0 OR CantidadSurtida = 0 OR PUVenta = 0 OR PUVendido = 0")
            '
            '
            'ES UNA VENTA ?
            '
            If vf_ContadorItems > 0 Then                                                                ' Es una Venta ?
                PasaVentaTemporalAVentas_Encabezado()                                                   'PASA PVDetalle --> Detalle
                prog_PasaVentaTemporalAVentas()
            End If
            '
            '
            'INGRESOS
            '
            If vf_Ventas_FolioAGrabar > 0 And Not _Cliente_InternoAplica Then
                CreaIngresoXVenta()
            End If
            '
            If _Importe_AdeudoAPagar > 0 And Not _Cliente_InternoAplica Then
                CreaIngresoXPagoDeAdeudo()
            End If
            '
            '
            'MONEDERO ELECTRONICO
            '
            If _ImportePago_ME > 0 Then                                                                 'Pago con su Monedero Electronico
                If _Cliente_Bloqueado Then                                                              'Cliente anonimo, presento tarjeta
                    If _Tarjeta_Id > 0 Then                                                             'Presento su tarjeta
                        'ok
                        cTarjeta.ME_SinClienteCreaTransaccion(_Cliente_Id, _Tarjeta_Id, 0, vf_Ventas_FolioAGrabar + 1, "Pago con ME", -(_ImportePago_ME))
                        cTarjeta.Tarjeta_ActualizaMEUsado(_Tarjeta_Id, _ImportePago_ME)
                    End If
                Else                                                                                    'Es un cliente que dieron de alta
                    cTarjeta.ME_ClienteCreaTransaccion(_Cliente_Id, _Tarjeta_Id, 0, vf_Ventas_FolioAGrabar + 1, "Pago con ME", -(_ImportePago_ME))
                    cTarjeta.ME_AplicaPagoDelCliente(_Cliente_Id, _ImportePago_ME)
                End If
                'Si pago con Monedero Electronico entonces no se le regalara Dinero Electronico
            Else                                                                                        'Acumula Dinero Electronico
                If _Cliente_Credito_Usado - _Importe_AdeudoAPagar > 0 Then                              'Continua debiendo, aun con abono
                    '
                Else

                    If cTarjeta.ME_ImportexEstaVenta <> 0 And _Cliente_ME_Aplica Then                       'Si es devolucion de mercancia el Importe ME podria ser negativo
                        If _Cliente_Bloqueado Then                                                          'Cliente anonimo
                            If _Tarjeta_Id > 0 Then                                                         'Presento su tarjeta
                                'ok
                                cTarjeta.ME_SinClienteCreaTransaccion(_Cliente_Id, _Tarjeta_Id, vf_Ventas_FolioAGrabar + 1, 0, "Acumulados por comprar", cTarjeta.ME_ImportexEstaVenta)
                                cTarjeta.Tarjeta_ActualizaMEAcumulado(_Tarjeta_Id, cTarjeta.ME_ImportexEstaVenta)
                            End If
                        Else                                                                                'Cliente dado de alta que acumula
                            cTarjeta.ME_ClienteCreaTransaccion(_Cliente_Id, _Tarjeta_Id, vf_Ventas_FolioAGrabar + 1, 0, "Acumulados por comprar", cTarjeta.ME_ImportexEstaVenta)
                            cTarjeta.Cliente_ActualizaMEAcumulado(_Cliente_Id, cTarjeta.ME_ImportexEstaVenta)
                        End If
                    End If
                End If
            End If
            '
            '
            'CREDITO SV
            '
            If _ImportePago_Credito > 0 Then                                                            'Uso su credito
                cTarjeta.Creditos_ClienteCreaTransaccion(_Cliente_Id, _Tarjeta_Id, vf_Ventas_FolioAGrabar + 1, "Pago con Tarjeta", _ImportePago_Credito, 0)
                cTarjeta.Cliente_ActualizaCreditoUsado(_Cliente_Id, _ImportePago_Credito)
            End If
            '
            If _Importe_AdeudoAPagar > 0 Then                                                           'Pago su adeudo (Credito usado)
                cTarjeta.Creditos_ClienteCreaTransaccion(_Cliente_Id, _Tarjeta_Id, vf_Ventas_FolioAGrabar + 1, "Pago de Adeudo", -1 * _Importe_AdeudoAPagar, 0)
                cTarjeta.Cliente_ActualizaCreditoUsado(_Cliente_Id, -1 * _Importe_AdeudoAPagar)
            End If
            '
            '
            'IMPRESION
            '
            If _ctrCobrando.pImprimirNotaFacturaNada = 1 Then
                Dim Ticket As cTicketDeVenta = New cTicketDeVenta
                Dim Nota As class_ImpresionNotaVenta = New class_ImpresionNotaVenta
                '
                If Mid(gTerminalBD_Ruta_Imagenes, 1, 11) = "MiniPrinter" Then
                    Ticket.prog_ImpresionNotaVenta(Str(vf_Ventas_FolioAGrabar + 1), Per_NotaVenta_NumeroImpresiones)
                Else
                    Nota.prog_ImpresionNotaVenta(Str(vf_Ventas_FolioAGrabar + 1), Per_NotaVenta_NumeroImpresiones)
                End If
            End If
            '
            '
            'REINICIANDO VARIABLES
            '
            prog_LimpiaTablaVentasTemporales()
            prog_InicializaVariablesXProducto()
            prog_InicializaVariablesXVenta()
            RecargaGridVentas()

            btn_FotoProducto.BackgroundImage = cLibreriasImagen.StringToImage(gPersonalizacion("Gen_FotoProductoX").ToString())
            'btn_FotoProducto.Text = Per_PV_Leyenda
            _Producto_Foto = ""
        End If
        vf_ContadorEnter = 0
        txtLeerCodigo.Focus()
        '
        'Fin Cobrando_EventCerrar()
    End Sub
    Private Sub AdministradorPendientes()
        PanelTrabajoDerecho_DeshabilitarControles()
        cClientePendientes = New ClientePendientes()

        Me.panelTrabajoDerecho.Controls.Add(cClientePendientes)
        cClientePendientes.Dock = DockStyle.Fill
        cClientePendientes.BringToFront()
    End Sub
    Private Sub AdministrarProductos()
        PanelTrabajoDerecho_DeshabilitarControles()
        cAdministrarProductos = New ctrAdministrarProductos()
        Me.panelTrabajoDerecho.Controls.Add(cAdministrarProductos)
        AddHandler cAdministrarProductos.btnCatalogoProductos_ClickEvent, AddressOf AbrirCatalogoProductos
        AddHandler cAdministrarProductos.btnImpresionPrecios_ClickEvent, AddressOf AbrirImpresionPrecios
        AddHandler cAdministrarProductos.btnCerrarClick, AddressOf AdministrarProductosCerrar
        'prog_ActualizaAdministrador(gConfigSistema_FechaActual)
        cAdministrarProductos.Dock = DockStyle.Fill
        cAdministrarProductos.BringToFront()
    End Sub
    Private Sub AbrirCatalogoProductos()
        PanelTrabajoDerecho_DeshabilitarControles()
        cCatProd = New ctrCatalogoProductos(_Producto_CodigoBarras)
        Me.panelTrabajoDerecho.Controls.Add(cCatProd)
        AddHandler cCatProd.CerrarCatalogoProductos, AddressOf CerrarCatalogoProductos
        cCatProd.Dock = DockStyle.Fill
        cCatProd.BringToFront()
    End Sub
    Private Sub AbrirImpresionPrecios()
        PanelTrabajoDerecho_DeshabilitarControles()
        'cImpresionPrecios = New ctrImprimirPrecios()
        Me.panelTrabajoDerecho.Controls.Add(cImpresionPrecios)
        'AddHandler cImpresionPrecios.btnCerrar_ClickEvent, AddressOf CerrarImpresionPrecios
        cImpresionPrecios.Dock = DockStyle.Fill
        cImpresionPrecios.BringToFront()
    End Sub
    Private Sub CerrarImpresionPrecios()
        RemoverControles_PanelTrabajoDerecho()
    End Sub
    Private Sub AdministrarProductosCerrar()
        RemoverControles_PanelTrabajoDerecho()
        'PanelTrabajoDerecho_HabilitarControles()
    End Sub
    Private Sub CerrarCatalogoProductos()
        Dim aceptar As Boolean = cCatProd.AceptarOk
        Dim Nombre As String = cCatProd.vf_ProductoSeleccionado_Nombre
        Dim Codigo As String = cCatProd.vf_ProductoSeleccionado_CodigoBarras
        RemoverControles_PanelTrabajoDerecho()
        Me.txtLeerCodigo.Focus()
        If aceptar Then
            If _Producto_Cantidad = 1 Or _Producto_Cantidad = -1 Then
                StatusStripPrincipal_Mensajes.Text = "Introduzca la Cantidad de " & Nombre
                txtLeerCodigo.Text = "*" & Trim(Str(Codigo))
                My.Computer.Keyboard.SendKeys("{HOME}", True)
            Else
                txtLeerCodigo.Text = Str(_Producto_Cantidad) & "*" & Trim(Str(cCatProd.vf_ProductoSeleccionado_CodigoBarras))
                My.Computer.Keyboard.SendKeys("{ENTER}", True)
            End If
            '
            Try
                'If Len(Trim(cCatProd.vf_ProductoSeleccionado_Foto)) = 0 Then
                '    btn_FotoProducto.BackgroundImage = Nothing
                '    btn_FotoProducto.Text = Per_PV_Leyenda
                '    _Producto_Foto = ""
                'Else
                '    btn_FotoProducto.BackgroundImage = Image.FromFile(gTerminalBD_Ruta_FotosProductos & "\" & cCatProd.vf_ProductoSeleccionado_Foto)
                '    btn_FotoProducto.Text = ""
                '    _Producto_Foto = cCatProd.vf_ProductoSeleccionado_Foto
                'End If
            Catch
            End Try
        End If
    End Sub
    Private Sub AcercaDe_EventCerrar()
        Me.panelTrabajo.Controls.Remove(_ctrAcercaDe)
        ToolStripPrincipal.Visible = True
        StatusStripPrincipal.Visible = True
        lnk_NombreComercial.Enabled = True
        PanelTrabajoDerecho_HabilitarControles()
    End Sub
    Private Sub FlujoEfectivo_EventCerrar()
        'Me.panelTrabajo.Controls.Remove(_ctrFlujoEfectivo)
        RemoverControles_PanelTrabajoDerecho()
    End Sub
    Private Sub RemoverControles_PanelTrabajoDerecho()
        If Me.panelTrabajoDerecho.Controls.Count() > _NumControlesEnPanelTrabajoDerecho Then
            Me.panelTrabajoDerecho.Controls.RemoveAt(0)
            If Me.panelTrabajoDerecho.Controls.Count = _NumControlesEnPanelTrabajoDerecho Then
                PanelTrabajoDerecho_HabilitarControles()
            End If
        End If
    End Sub
    Private Sub CatalogoClientes_Cerrar()
        Dim ClienteSeleccionado As Boolean
        ClienteSeleccionado = cCatalogoClientes._ClienteSeleccionadoOk
        Dim ClienteSeleccionado_Clave As Integer = 0
        ClienteSeleccionado_Clave = cCatalogoClientes.vf_ClienteSeleccionado_Clave
        '
        RemoverControles_PanelTrabajoDerecho()
        '
        If ClienteSeleccionado Then
            vf_ClienteYaSeleccionado = True
            InicializaDatosTarjeta()
            prog_ObtenerDatosClienteAVisualizar(ClienteSeleccionado_Clave)
        End If
    End Sub
    Private Sub ClientePendientes_Abrir()
        PanelTrabajoDerecho_DeshabilitarControles()
        '
        cClientePendientes = New ClientePendientes()
        Me.panelTrabajoDerecho.Controls.Add(cClientePendientes)
        'AddHandler cCatalogoClientes.ClickBotonesTerminar, AddressOf CatalogoClientes_Cerrar
        'AddHandler cCatalogoClientes.ClickBotonPendientes, AddressOf ClientePendientes_Abrir
        cClientePendientes.Dock = DockStyle.Fill
        cClientePendientes.BringToFront()
    End Sub
#End Region

#Region "ToolStripPanelAdminsitrador"
    Private Sub ToolStripPanelAdministrador_Activar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripPanelAdministrador_botonActivar.Click
        If gUsuario_Admin_PanelAdministrador Then
            If Me.SplitContainerTrabajo.SplitterDistance = 175 Then
                Me.panelTrabajoIzquierdoInterno.Visible = False
                Me.SplitContainerTrabajo.SplitterDistance = 33
                Me.ToolStripPanelAdministrador.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow
                Me.ToolStripPanelAdministrador_botonActivar.TextImageRelation = TextImageRelation.ImageAboveText
                Me.ToolStripPanelAdministrador_botonActivar.TextDirection = ToolStripTextDirection.Vertical270
            Else
                Me.panelTrabajoIzquierdoInterno.Visible = True
                Me.ToolStripPanelAdministrador.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow
                Me.ToolStripPanelAdministrador_botonActivar.TextImageRelation = TextImageRelation.TextBeforeImage
                Me.ToolStripPanelAdministrador_botonActivar.TextDirection = ToolStripTextDirection.Horizontal
                Me.SplitContainerTrabajo.SplitterDistance = 175
            End If
        Else
            cLibreriasMensaje.Mostrar("El usuario no tiene permisos para Accesar el Panel del Administrador", Mensajes.Emergente.Error)
        End If
    End Sub
    Private Sub ToolStripPanelAdministrador_Minimizar()
        Me.panelTrabajoIzquierdoInterno.Visible = False
        Me.SplitContainerTrabajo.SplitterDistance = 33
        Me.ToolStripPanelAdministrador.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow
        Me.ToolStripPanelAdministrador_botonActivar.TextImageRelation = TextImageRelation.ImageAboveText
        Me.ToolStripPanelAdministrador_botonActivar.TextDirection = ToolStripTextDirection.Vertical270
    End Sub
#End Region

End Class
