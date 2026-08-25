'
'27 Escape
'38 Cursor arriba
'40 Cursor abajo
'   Tecla <+>
'
Imports System.Drawing
Imports System.Drawing.Drawing2D
Public Class Cobrando

#Region "Variables"
    '
    Public pImprimirNotaFacturaNada As Integer = 0     ' 0 = Nada, 1 = ImprimirNota, 2 = Generar Factura
    Private _lFormatoCantidad As String = "##,###,##0.00"
    Private vfME_MinimoCambiar As Decimal = 0
    '
    Private _ClienteNoTarjeta As String = ""
    Private _ClienteClave As String
    Private _ClienteNombre As String
    Private _ClienteImagen As String = ""
    Private _ClienteLimiteCredito As Decimal = 0
    Private _ClienteCreditoUsado As Decimal = 0
    Private _ImportePositivoVenta As Decimal = 0
    Private _ImporteNegativoVenta As Decimal = 0
    '
    Public _ImporteEfectivoEntregado As Decimal
    Public _ImportePago_ME As Decimal = 0
    Public _ImportePago_Credito As Decimal = 0
    'Public _ImportePago_Otros As Decimal = 0
    Public _ImportePago_Referencia As String = ""
    Public _ImportePago_TOTAL As Decimal = 0
    Public _Importe_Vuelto As Decimal = 0
    Public _Importe_PagoAdeudo As Decimal = 0
    '
    Public _AplicaME As Boolean = False
    Public _AplicaCredito As Boolean = False
    '
    Public _CobradoOk As Boolean = False
    '
    'Variable Auxiliar para controlar el tamaño de las etiquetas o labels que parpadean, aumentando y disminuyendo su tamaño
    Private _LetraTamano As Decimal = 9
    Private _LetraAumentando = True
    '
    Private _controls As String = ""
#End Region

#Region "Variables de Clase"
    'Private _Cobrando_EntreMECS As Cobrando_EntreMECS = New Cobrando_EntreMECS("", 0, 0)
    'Private cClientePendientes As ClientePendientes = New ClientePendientes("", 0, 0)
#End Region

#Region "Eventos"
    Public Event ClickBotonClientePendientes As EventHandler
    Public Event ClickBotonesTerminar As EventHandler
#End Region

#Region "Public New"
    Public Sub New(ByVal pCliente_Clave As String, _
                   ByVal pCliente_Nombre As String, _
                   ByVal pCliente_Imagne As String, _
                   ByVal pCliente_CreditoAplica As Boolean, _
                   ByVal pCliente_CreditoLimite As Decimal, _
                   ByVal pCliente_CreditoUsado As Decimal, _
                   ByVal pCliente_CreditoBloquearSiDEbe As Boolean, _
                   ByVal pCliente_MEAplica As Boolean, _
                   ByVal pCliente_ME_Disponible As Decimal, _
                   ByVal pCliente_Bloqueado As Boolean, _
                   ByVal pImportePositivo_Venta As Decimal, _
                   ByVal pImporteNegativo_Venta As Decimal, _
                   ByVal pImportePago_Efectivo As Decimal, _
                   ByVal pImportePago_ME As Decimal, _
                   ByVal pImportePago_Credito As Decimal, _
                   ByVal pImportepago_referencia As String, _
                   ByVal pImportePago_TOTAL As Decimal, _
                   ByVal pImporte_Vuelto As Decimal, _
                   ByVal pImporte_PagoAdeudo As Decimal, _
                   ByVal pImporte_ME_xEstaVenta As Decimal, _
                   ByVal pPer_MonedoeroElectronico_Aplica As Boolean, _
                   ByVal pPer_Credito_Aplica As Boolean, _
                   ByVal pPer_Credito_MostarLimiteYDisponible As Boolean, _
                   ByVal pPer_ME_ClienteCriterioAplicacion As String, _
                   ByVal pCliente_NoTarjeta As String, _
                   ByVal pCliente_existe As Boolean, _
                   ByVal pCliente_ClienteId As Integer, _
                   ByVal pTarjeta_MEAplica As Boolean, _
                   ByVal pTarjeta_MESaldo As Decimal, _
                   ByVal pTarjeta_CreditoAplica As Boolean, _
                   ByVal pTarjeta_CreditoLimite As Decimal, _
                   ByVal pTarjeta_CreditoUsado As Decimal, _
                   ByVal pTarjeta_CreditoDisponible As Decimal, _
                   ByVal pTarjeta_Cancelada As Boolean, _
                   ByVal pCobradoOk As Boolean)
        Try
            InitializeComponent()
            _ClienteNoTarjeta = pCliente_NoTarjeta
            _ClienteClave = pCliente_Clave
            _ClienteNombre = pCliente_Nombre
            _ClienteImagen = pCliente_Imagne
            _ClienteLimiteCredito = pCliente_CreditoLimite
            _ClienteCreditoUsado = pCliente_CreditoUsado
            '
            _ImportePositivoVenta = pImportePositivo_Venta
            _ImporteNegativoVenta = pImporteNegativo_Venta
            '
            _ImporteEfectivoEntregado = pImportePago_Efectivo
            _ImportePago_ME = pImportePago_ME
            _ImportePago_Credito = pImportePago_Credito
            '_ImportePago_Otros = pImportePago_Otros
            _ImportePago_Referencia = pImportepago_referencia
            _ImportePago_TOTAL = pImportePago_TOTAL
            _Importe_Vuelto = pImporte_Vuelto
            _Importe_PagoAdeudo = pImporte_PagoAdeudo
            '
            Me.labelEfectivoRecibido.Visible = pCobradoOk
            '
            'MONEDERO ELECTRONICO
            'Personalizacion. Aplica Monedoero Electronico
            If pPer_MonedoeroElectronico_Aplica Then
                'Personalizacion. Criterio de aplicacion para el cliente
                If pPer_ME_ClienteCriterioAplicacion = "Todos" Then
                    txtCobrandoME.Enabled = True
                    label_ME_ImporteEstaVenta.Text = Format(pImporte_ME_xEstaVenta, _lFormatoCantidad)
                    label_ME_Disponible.Text = Format(cLibreriasDatos.ElMasAlto(pCliente_ME_Disponible, pTarjeta_MESaldo), _lFormatoCantidad)
                    _AplicaME = True
                    Me.txtCobrandoME.Enabled = True
                Else
                    'Personalizacion. Criterio de aplicacion: "Segun Perfil del cliente"
                    If pCliente_MEAplica Then
                        txtCobrandoME.Enabled = True
                        label_ME_ImporteEstaVenta.Text = Format(pImporte_ME_xEstaVenta, _lFormatoCantidad)
                        label_ME_Disponible.Text = Format(cLibreriasDatos.ElMasAlto(pCliente_ME_Disponible, pTarjeta_MESaldo), _lFormatoCantidad)
                        _AplicaME = True
                        Me.txtCobrandoME.Enabled = True
                    Else
                        Me.panelMonederoElectronico.Visible = False
                        txtCobrandoME.Enabled = False
                        label_ME_ImporteEstaVenta.Text = Format(0, _lFormatoCantidad)
                        label_ME_Disponible.Text = Format(0, _lFormatoCantidad)
                        _AplicaME = False
                        Me.txtCobrandoME.Enabled = False
                    End If
                End If
            Else
                Me.panelMonederoElectronico.Visible = False
                txtCobrandoME.Enabled = False
                label_ME_ImporteEstaVenta.Text = Format(pImporte_ME_xEstaVenta, _lFormatoCantidad)
                label_ME_Disponible.Text = Format(0, _lFormatoCantidad)
                _AplicaME = False
                Me.txtCobrandoME.Enabled = False
            End If
            '
            'CREDITO
            If pPer_Credito_Aplica Then
                If pCliente_CreditoAplica Or pTarjeta_CreditoAplica Then    'Si el cliente o la Tarjeta aplica para Credito
                    Me.panelCreditoSuperventas.Visible = True
                    _AplicaCredito = True
                    Me.txtCobrandoCredito.Enabled = True
                    Me.labelCreditoLimite.Text = Format(cLibreriasDatos.ElMasAlto(pCliente_CreditoLimite, pTarjeta_CreditoLimite), _lFormatoCantidad)
                    Me.labelCreditoUsado.Text = Format(cLibreriasDatos.ElMasAlto(pCliente_CreditoUsado, pTarjeta_CreditoUsado), _lFormatoCantidad)
                    Me.labelCreditoDisponible.Text = Format(cLibreriasDatos.ElMasAlto(pCliente_CreditoLimite - pCliente_CreditoUsado, pTarjeta_CreditoDisponible), _lFormatoCantidad)
                    '
                    If _ClienteCreditoUsado > 0 Then
                        Me.timerParpadeaAdeudo.Start()
                        Me.timerParpadeaAdeudo.Enabled = True
                    Else
                        Me.labelAdeudoAPagar.Visible = False
                        Me.txtAdeudoAPagar.Visible = False
                        '
                        Me.timerParpadeaAdeudo.Stop()
                        Me.timerParpadeaAdeudo.Enabled = False
                    End If
                    '
                    If Not pPer_Credito_MostarLimiteYDisponible Then
                        Me.labelCreditoLimite_.Visible = False
                        Me.labelCreditoLimite.Visible = False
                        Me.labelCreditoDisponible_.Visible = False
                        Me.labelCreditoDisponible.Visible = False
                    End If
                Else
                    Me.panelCreditoSuperventas.Visible = False
                    _AplicaCredito = False
                    Me.txtCobrandoCredito.Enabled = False
                    '
                    Me.labelAdeudoAPagar.Visible = False
                    Me.txtAdeudoAPagar.Visible = False
                    '
                    Me.timerParpadeaAdeudo.Stop()
                    Me.timerParpadeaAdeudo.Enabled = False
                End If
            Else
                Me.panelCreditoSuperventas.Visible = False
                _AplicaCredito = False
                Me.txtCobrandoCredito.Enabled = False
                '
                Me.labelAdeudoAPagar.Visible = False
                Me.txtAdeudoAPagar.Visible = False
                '
                Me.timerParpadeaAdeudo.Stop()
                Me.timerParpadeaAdeudo.Enabled = False
            End If
        Catch
            MessageBox.Show(pCliente_Clave & "-" & _
                    pCliente_Nombre & "-" & _
                    pCliente_Imagne & "-" & _
                    pCliente_CreditoAplica.ToString() & "-" & _
                    pCliente_CreditoLimite.ToString() & "-" & _
                    pCliente_CreditoUsado.ToString() & "-" & _
                    pCliente_CreditoBloquearSiDEbe.ToString() & "-" & _
                    pCliente_MEAplica.ToString() & "-" & _
                    pCliente_ME_Disponible.ToString() & "-" & _
                    pCliente_Bloqueado.ToString() & "-" & _
                    pImportePositivo_Venta.ToString() & "-" & _
                    pImporteNegativo_Venta.ToString() & "-" & _
                    pImportePago_Efectivo.ToString() & "-" & _
                    pImportePago_ME.ToString() & "-" & _
                    pImportePago_Credito.ToString() & "-" & _
                    pImportepago_referencia & "-" & _
                    pImportePago_TOTAL.ToString() & "-" & _
                    pImporte_Vuelto & "-" & _
                    pImporte_PagoAdeudo.ToString() & "-" & _
                    pImporte_ME_xEstaVenta.ToString() & "-" & _
                    pPer_MonedoeroElectronico_Aplica.ToString() & "-" & _
                    pPer_Credito_Aplica.ToString() & "-" & _
                    pPer_Credito_MostarLimiteYDisponible.ToString() & "-" & _
                    pPer_ME_ClienteCriterioAplicacion & "-" & _
                    pCliente_NoTarjeta & "-" & _
                    pCliente_existe.ToString() & "-" & _
                    pCliente_ClienteId.ToString() & "-" & _
                    pTarjeta_MEAplica.ToString() & "-" & _
                    pTarjeta_MESaldo.ToString() & "-" & _
                    pTarjeta_CreditoAplica.ToString() & "-" & _
                    pTarjeta_CreditoLimite.ToString() & "-" & _
                    pTarjeta_CreditoUsado.ToString() & "-" & _
                    pTarjeta_CreditoDisponible.ToString() & "-" & _
                    pTarjeta_Cancelada.ToString() & "-" & _
                    pCobradoOk.ToString())
        End Try
    End Sub
#End Region

#Region "Cobrando"
    Private Sub Cobrando_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = 38 Then
            My.Computer.Keyboard.SendKeys("+{TAB}", True)
        End If
    End Sub

    Private Sub Cobrando_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = "" Then
            My.Computer.Keyboard.SendKeys("+{TAB}", True)
        End If
    End Sub

    Private Sub Cobrando_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = 38 Then
            My.Computer.Keyboard.SendKeys("+{TAB}", True)
        End If
    End Sub

    Private Sub Cobrando_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim linGrBrush As New LinearGradientBrush(New Point(0, 0), New Point(Me.Width, Me.Height), Color.FromArgb(255, 255, 150), Color.White)
        'Dim pen As New Pen(linGrBrush)

        'e.Graphics.DrawLine(pen, 0, 10, 200, 10)
        'e.Graphics.FillEllipse(linGrBrush, 0, 30, 200, 100)
        e.Graphics.FillRectangle(linGrBrush, 0, 0, Me.Width, Me.Height)
    End Sub

    Private Sub Cobrando_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _CobradoOk = False
        labelClienteClave.Text = _ClienteClave
        labelClienteNombre.Text = _ClienteNombre
        picClienteFoto.BackgroundImage = cLibreriasImagen.StringToImage(_ClienteImagen)
        '
        txtCobrandoCredito.Visible = True
        boxImporteVenta.Text = Format((_ImportePositivoVenta + _ImporteNegativoVenta), _lFormatoCantidad)
        Me.txtCobrandoEfectivoRecibido.Text = Format(_ImporteEfectivoEntregado, _lFormatoCantidad)
        Me.txtCobrandoME.Text = Format(_ImportePago_ME, _lFormatoCantidad)
        Me.txtCobrandoCredito.Text = Format(_ImportePago_Credito, _lFormatoCantidad)
        Me.txtAdeudoAPagar.Text = Format(_Importe_PagoAdeudo, _lFormatoCantidad)
        '
        labelCreditoLimite.Text = Format(_ClienteLimiteCredito, _lFormatoCantidad)
        labelCreditoUsado.Text = Format(_ClienteCreditoUsado, _lFormatoCantidad)
        '
        RecalculaImportes()
        txtCobrandoEfectivoRecibido.Focus()
        '
        Me.timerParpadeaLables.Start()
    End Sub
#End Region

#Region "txtEfectivoRecibido"

    Private Sub txtCobrandoEfectivoRecibido_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCobrandoEfectivoRecibido.Enter
        Me.txtCobrandoEfectivoRecibido.BackColor = Color.Red
        Me.txtCobrandoEfectivoRecibido.ForeColor = Color.Yellow
        '

    End Sub
    Private Sub txtCobrandoEfectivo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCobrandoEfectivoRecibido.KeyDown
        'If e.KeyCode = 27 Then
        '    RaiseEvent ClickBotonesTerminar(sender, e)
        'End If
        '
        If e.KeyCode = 40 Then
            My.Computer.Keyboard.SendKeys("{TAB}", True)
        End If
    End Sub
    Private Sub txtCobrandoEfectivo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCobrandoEfectivoRecibido.KeyPress
        'ChecarTeclaPresionada(sender, e)
        If InStr(1, "+", e.KeyChar) > 0 Then    'Si oprimio "+" entonces salir
            e.KeyChar = ""
            RaiseEvent ClickBotonesTerminar(sender, e)
        Else
            If InStr(1, Chr(13), e.KeyChar) > 0 Then  'Oprimio Enter
                My.Computer.Keyboard.SendKeys("{TAB}", True)
            Else
                If InStr(1, "-1234567890." & Chr(8), e.KeyChar) = 0 Then
                    e.KeyChar = ""
                Else
                    Me.labelEfectivoRecibido.Visible = True
                End If
            End If
        End If
    End Sub
    Private Sub txtCobrandoEfectivo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCobrandoEfectivoRecibido.Leave
        txtCobrandoEfectivoRecibido.Text = Format(cLibreriasDatos.ValorDecimal(txtCobrandoEfectivoRecibido.Text), _lFormatoCantidad)
        RecalculaImportes()
        If cLibreriasDatos.ValorDecimal(boxImportePagoCliente_TOTAL.Text) > cLibreriasDatos.ValorDecimal(txtCobrandoEfectivoRecibido.Text) Then
            '_VentanaEmergente = New Mensajes.Emergente("El Vuelto NO PUEDE ser mayor al Pago en Efectivo", Mensajes.Emergente.Error)
            '_VentanaEmergente.Show()
        End If
        '
        Me.txtCobrandoEfectivoRecibido.BackColor = Color.Salmon
        Me.txtCobrandoEfectivoRecibido.ForeColor = Color.Black
        '
        labelsTxtVisible()
    End Sub
#End Region

#Region "txtCobrando ME"

    Private Sub txtCobrandoME_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCobrandoME.Enter
        Me.txtCobrandoME.BackColor = Color.Red
        Me.txtCobrandoME.ForeColor = Color.Yellow
    End Sub
    Private Sub txtCobrandoME_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCobrandoME.KeyDown
        If e.KeyCode = 40 Then                          'Cursor abajo
            My.Computer.Keyboard.SendKeys("{TAB}", True)
        Else
            If e.KeyCode = 38 Then                      'Cursor arriba  
                My.Computer.Keyboard.SendKeys("+{TAB}", True)
            End If
        End If
    End Sub
    Private Sub txtCobrandoME_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCobrandoME.KeyPress
        If InStr(1, "+", e.KeyChar) > 0 Then                                'Si oprimio "+" entonces salir
            e.KeyChar = ""                                                  'Ofuscar eco
            RaiseEvent ClickBotonesTerminar(sender, e)
        Else
            If InStr(1, Chr(13), e.KeyChar) > 0 Then                        'Oprimio Enter
                My.Computer.Keyboard.SendKeys("{TAB}", True)
            Else
                Dim CadenaValida As String
                If gUsuario_Perm_MonederoElectronico_PuedeCanjear Then
                    CadenaValida = ""
                Else
                    CadenaValida = gPersonalizacion("TarjetaSV_PrefijoNumeroTarjeta").ToString()
                End If
                If InStr(1, CadenaValida & "-1234567890." & Chr(8), e.KeyChar) > 0 Then

                Else                                                        'Tecla invalida
                    e.KeyChar = ""                                          'Ofuscar eco
                End If
            End If
        End If
    End Sub
    Private Sub txtCobrandoME_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCobrandoME.Leave
        If Not gUsuario_Perm_MonederoElectronico_PuedeCanjear Then
            txtCobrandoME.Clear()
            txtCobrandoME.Text = "0.00"
        End If
        '
        Me.txtCobrandoME.Text = Format(cLibreriasDatos.ValorDecimal(Me.txtCobrandoME.Text), _lFormatoCantidad)
        RecalculaImportes()
        '
        Me.txtCobrandoME.BackColor = Color.Salmon
        Me.txtCobrandoME.ForeColor = Color.Black
        '
        labelsTxtVisible()
    End Sub
    'txtCobrando ME
#End Region

#Region "btnPendientes"
    Private Sub btnPendientes_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPendientes.GotFocus
        If Me.txtAdeudoAPagar.Visible Then
            Me.txtAdeudoAPagar.Focus()
        Else
            Me.btnTerminar.Focus()
        End If

    End Sub

    Private Sub btnPendientes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPendientes.Click
        RaiseEvent ClickBotonClientePendientes(sender, e)
    End Sub
#End Region

#Region "txtCobrandoCredito"

    Private Sub txtCobrandoCredito_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCobrandoCredito.Enter
        Me.txtCobrandoCredito.BackColor = Color.Red
        Me.txtCobrandoCredito.ForeColor = Color.Yellow
    End Sub
    Private Sub txtCobrandoCredito_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCobrandoCredito.KeyDown
        'If e.KeyCode = 27 Then
        '    RaiseEvent ClickBotonesTerminar(sender, e)
        'End If
        '
        If e.KeyCode = 40 Then                          'Cursor abajo
            My.Computer.Keyboard.SendKeys("{TAB}", True)
        End If
        '
        If e.KeyCode = 38 Then                          'Cursor arriba
            My.Computer.Keyboard.SendKeys("+{TAB}", True)
        End If
    End Sub
    Private Sub txtCobrandoCredito_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCobrandoCredito.KeyPress
        If InStr(1, "+", e.KeyChar) > 0 Then                                'Si oprimio "+" entonces salir
            e.KeyChar = ""                                                  'Ofuscar eco
            RaiseEvent ClickBotonesTerminar(sender, e)
        Else
            If InStr(1, Chr(13), e.KeyChar) > 0 Then                        'Oprimio Enter
                My.Computer.Keyboard.SendKeys("{TAB}", True)
            Else
                If InStr(1, "-1234567890." & Chr(8), e.KeyChar) > 0 Then

                Else                                                        'Tecla invalida
                    e.KeyChar = ""                                          'Ofuscar eco
                End If
            End If
        End If
    End Sub
    Private Sub txtCobrandoCredito_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCobrandoCredito.Leave
        Me.txtCobrandoCredito.Text = Format(cLibreriasDatos.ValorDecimal(Me.txtCobrandoCredito.Text), _lFormatoCantidad)
        RecalculaImportes()
        '
        Me.txtCobrandoCredito.BackColor = Color.Salmon
        Me.txtCobrandoCredito.ForeColor = Color.Black
        '
        labelsTxtVisible()
    End Sub
    'txtCobrandoCredito
#End Region

#Region "txtAdeudoAPagar"
    Private Sub txtAdeudoAPagar_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdeudoAPagar.Enter
        Me.txtAdeudoAPagar.BackColor = Color.Red
        Me.txtAdeudoAPagar.ForeColor = Color.Yellow
    End Sub
    Private Sub txtAdeudoAPagar_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAdeudoAPagar.KeyDown
        If e.KeyCode = 40 Then                          'Cursor abajo
            My.Computer.Keyboard.SendKeys("{TAB}", True)
        End If
        '
        If e.KeyCode = 38 Then                          'Cursor arriba
            If Me.txtCobrandoCredito.Enabled Then
                Me.txtCobrandoCredito.Focus()
            Else
                If Me.txtCobrandoME.Enabled Then
                    Me.txtCobrandoME.Focus()
                Else
                    Me.txtCobrandoEfectivoRecibido.Focus()
                End If
            End If
            'My.Computer.Keyboard.SendKeys("+{TAB}", True)
        End If
    End Sub
    Private Sub txtAdeudoAPagar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdeudoAPagar.KeyPress
        If InStr(1, "+", e.KeyChar) > 0 Then                                'Si oprimio "+" entonces salir
            e.KeyChar = ""                                                  'Ofuscar eco
            RaiseEvent ClickBotonesTerminar(sender, e)
        Else
            If InStr(1, Chr(13), e.KeyChar) > 0 Then                        'Oprimio Enter
                My.Computer.Keyboard.SendKeys("{TAB}", True)
            Else
                If InStr(1, "-1234567890." & Chr(8), e.KeyChar) > 0 Then

                Else                                                        'Tecla invalida
                    e.KeyChar = ""                                          'Ofuscar eco
                End If
            End If
        End If
    End Sub
    Private Sub txtAdeudoAPagar_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdeudoAPagar.Leave
        Me.txtAdeudoAPagar.Text = Format(cLibreriasDatos.ValorDecimal(Me.txtAdeudoAPagar.Text), _lFormatoCantidad)
        RecalculaImportes()
        '
        Me.txtAdeudoAPagar.BackColor = Color.Salmon
        Me.txtAdeudoAPagar.ForeColor = Color.Black
        '
        labelsTxtVisible()
    End Sub
    'txtAdeudoAPagar
#End Region

#Region "btnTerminar"

    Private Sub btnTerminar_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTerminar.Enter
        RecalculaImportes()
        '
        Me.labelVuelto_SaldoNuevo.BackColor = Color.Red
        Me.labelVuelto_SaldoNuevo.ForeColor = Color.Yellow
        Me.boxVuelto.BackColor = Color.Red
        Me.boxVuelto.ForeColor = Color.Yellow
    End Sub

    Private Sub btnTerminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTerminar.Click
        RecalculaImportes()
        pImprimirNotaFacturaNada = 0 'No imprimir. Solo terminar
        If ValidaDatosParaTerminar() Then
            RaiseEvent ClickBotonesTerminar(sender, e)
        End If
    End Sub
    Private Sub btnTerminar_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnTerminar.KeyDown
        'If e.KeyCode = 27 Then
        '    RaiseEvent ClickBotonesTerminar(sender, e)
        'End If
        '
        If e.KeyCode = 38 Then
            'My.Computer.Keyboard.SendKeys("+{TAB}", True)
        End If
    End Sub
    Private Sub btnTerminar_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnTerminar.KeyUp
        If e.KeyCode = 38 Then
            If Me.txtAdeudoAPagar.Visible Then
                Me.txtAdeudoAPagar.Focus()
            Else
                If Me.txtCobrandoCredito.Enabled Then
                    Me.txtCobrandoCredito.Focus()
                Else
                    If Me.txtCobrandoME.Enabled Then
                        Me.txtCobrandoME.Focus()
                    Else
                        Me.txtCobrandoEfectivoRecibido.Focus()
                    End If
                End If
            End If
            'My.Computer.Keyboard.SendKeys("+{TAB}", True)
        End If
    End Sub
    Private Sub btnTerminar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles btnTerminar.KeyPress
        ChecarTeclaPresionada(sender, e)
    End Sub
    Private Sub btnTerminar_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTerminar.Leave
        Me.labelVuelto_SaldoNuevo.BackColor = Color.FromArgb(255, 255, 128)
        Me.labelVuelto_SaldoNuevo.ForeColor = Color.Black
        Me.boxVuelto.BackColor = Color.FromArgb(224, 224, 224)
        Me.boxVuelto.ForeColor = Color.Black
    End Sub
    'btnTerminar
#End Region

#Region "btnTerminarEImprimir"
    Private Sub btnTerminarEImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTerminarEImprimir.Click
        RecalculaImportes()
        pImprimirNotaFacturaNada = 1 'Imprimir Nota
        If ValidaDatosParaTerminar() Then
            RaiseEvent ClickBotonesTerminar(sender, e)
        End If
    End Sub
    Private Sub btnTerminarEImprimir_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnTerminarEImprimir.KeyDown
        'If e.KeyCode = 27 Then
        '    RaiseEvent ClickBotonesTerminar(sender, e)
        'End If
        '
    End Sub
    Private Sub btnTerminarEImprimir_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles btnTerminarEImprimir.KeyPress
        ChecarTeclaPresionada(sender, e)
    End Sub
#End Region

#Region "btnCancelar"
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        RecalculaImportes()
        _CobradoOk = False
        RaiseEvent ClickBotonesTerminar(sender, e)
    End Sub

    Private Sub btnCancelar_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnCancelar.KeyDown
        'If e.KeyCode = 27 Then
        '    RaiseEvent ClickBotonesTerminar(sender, e)
        'End If
        '
    End Sub

    Private Sub btnCancelar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles btnCancelar.KeyPress
        ChecarTeclaPresionada(sender, e)
    End Sub
#End Region

#Region "Funciones"
    Private Sub RecalculaImportes()
        'EfectivoRecibido + CobrandoME + CobrandoCredito
        boxImportePagoCliente_TOTAL.Text = Format(cLibreriasDatos.ValorDecimal(txtCobrandoEfectivoRecibido.Text) + cLibreriasDatos.ValorDecimal(txtCobrandoME.Text) + cLibreriasDatos.ValorDecimal(txtCobrandoCredito.Text), _lFormatoCantidad)
        'txtImporteAAbonar.Text = boxImportePagoCliente_TOTAL.Text
        'ImportePagoClienteTOTAL - AdeudoAPagar - (ImportePositivoVenta + ImporteNegativoVenta)
        boxVuelto.Text = Format(cLibreriasDatos.ValorDecimal(boxImportePagoCliente_TOTAL.Text) - cLibreriasDatos.ValorDecimal(Me.txtAdeudoAPagar.Text) - (_ImportePositivoVenta + _ImporteNegativoVenta), _lFormatoCantidad)
    End Sub

    Private Sub RecalculaSaldoNuevoCliente()
        ''boxImportePagoCliente_TOTAL.Text = Format(ValorDecimal(txtCobrandoEfectivo.Text) + ValorDecimal(txtCobrandoME.Text) + ValorDecimal(txtCobrandoCS.Text) + ValorDecimal(txtCobrandoOtros.Text), _lFormatoCantidad)
        ''txtImporteAAbonar.Text = boxImportePagoCliente_TOTAL.Text
        'boxVuelto.Text = Format(ValorDecimal(boxImporteVenta_Saldo.Text) - ValorDecimal(txtImporteAAbonar.Text), _lFormatoCantidad)
    End Sub

    Private Sub ChecarTeclaPresionada(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If InStr(1, "+", e.KeyChar) > 0 Then    'Si oprimio "+" entonces salir
            e.KeyChar = ""
            RaiseEvent ClickBotonesTerminar(sender, e)
        Else
            If InStr(1, Chr(13), e.KeyChar) > 0 Then  'Oprimio Enter
                My.Computer.Keyboard.SendKeys("{TAB}", True)
            Else
                If InStr(1, "-1234567890." & Chr(8), e.KeyChar) = 0 Then
                    e.KeyChar = ""
                End If
            End If
        End If
    End Sub
    Private Function ValidaDatosParaTerminar() As Boolean
        Dim Evaluacion As Boolean = False
        If (cLibreriasDatos.ValorDecimal(boxVuelto.Text) > cLibreriasDatos.ValorDecimal(txtCobrandoEfectivoRecibido.Text)) And cLibreriasDatos.ValorDecimal(boxImporteVenta.Text) > 0 Then
            cLibreriasMensaje.Mostrar("El Vuelto NO PUEDE ser mayor al Pago en Efectivo", Mensajes.Emergente.Error)
        Else
            If cLibreriasDatos.ValorDecimal(boxVuelto.Text) < 0 And cLibreriasDatos.ValorDecimal(boxImporteVenta.Text) > 0 Then
                cLibreriasMensaje.Mostrar("El Pago del Cliente esta incompleto", Mensajes.Emergente.Error)
            Else
                If cLibreriasDatos.ValorDecimal(txtCobrandoME.Text) > cLibreriasDatos.ValorDecimal(label_ME_Disponible.Text) Then
                    cLibreriasMensaje.Mostrar("El Pago con el Monedero Electronico excede su Total Acumulado", Mensajes.Emergente.Error)
                Else
                    If cLibreriasDatos.ValorDecimal(txtCobrandoCredito.Text) > cLibreriasDatos.ValorDecimal(Me.labelCreditoDisponible.Text) Then
                        cLibreriasMensaje.Mostrar("El Pago con Credito excede su Total Acumulado", Mensajes.Emergente.Error)
                    Else
                        _CobradoOk = True
                        Evaluacion = True
                    End If
                End If
            End If
        End If
        Return Evaluacion
    End Function
#End Region

#Region "timer_tick"
    Private Sub timerParpadeaEtiquetas_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerParpadeaAdeudo.Tick
        If Me.labelCreditoUsado.Visible Then
            Me.labelCreditoUsado.Visible = False
            Me.txtAdeudoAPagar.ForeColor = Color.Yellow
        Else
            Me.labelCreditoUsado.Visible = True
            Me.txtAdeudoAPagar.ForeColor = Color.White
        End If
        '
        If _LetraAumentando Then
            _LetraTamano = _LetraTamano + 0.5
        Else
            _LetraTamano = _LetraTamano - 0.5
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
        Me.labelCreditoUsado.Font = New Font("Microsoft Sans Serif", _LetraTamano, FontStyle.Bold)
    End Sub
#End Region

#Region "timerParpadeaLabels"
    Private Sub timerParpadeaLables_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerParpadeaLables.Tick

        For Each ctrl In Me.tlpImportes.Controls
            If (ctrl.GetType() Is GetType(System.Windows.Forms.TextBox)) Then
                Dim txt As System.Windows.Forms.TextBox = CType(ctrl, System.Windows.Forms.TextBox)
                If txt.Focused Then
                    Select Case txt.Name
                        Case Is = "txtCobrandoEfectivoRecibido"
                            If Me.labelCobrandoEfectivoRecibido.Visible Then
                                Me.labelCobrandoEfectivoRecibido.Visible = False
                            Else
                                Me.labelCobrandoEfectivoRecibido.Visible = True
                            End If
                        Case Is = "txtCobrandoME"
                            If Me.labelCobrandoME.Visible Then
                                Me.labelCobrandoME.Visible = False
                            Else
                                Me.labelCobrandoME.Visible = True
                            End If
                        Case Is = "txtCobrandoCredito"
                            If Me.labelCobrandoCS.Visible Then
                                Me.labelCobrandoCS.Visible = False
                            Else
                                Me.labelCobrandoCS.Visible = True
                            End If
                        Case Is = "txtAdeudoAPagar"
                            If _ClienteCreditoUsado > 0 Then
                                If Me.labelAdeudoAPagar.Visible Then
                                    Me.labelAdeudoAPagar.Visible = False
                                Else
                                    Me.labelAdeudoAPagar.Visible = True
                                End If
                            Else

                            End If
                    End Select
                Else
                    'Me.labelCobrandoEfectivoRecibido.Visible = True
                    'Me.labelCobrandoME.Visible = True
                    'Me.labelCobrandoCS.Visible = True
                    'Me.labelAdeudoAPagar.Visible = True
                End If
                'txt.Text = ""
            End If
        Next

        'Select Case _controls
        '    Case Is = "Efectivo"
        '        If Me.txtCobrandoEfectivoRecibido.Visible Then
        '            Me.txtCobrandoEfectivoRecibido.Visible = False
        '        Else
        '            Me.txtCobrandoEfectivoRecibido.Visible = True
        '        End If
        '    Case Is = "ME"
        '        If Me.txtCobrandoME.Visible Then
        '            Me.txtCobrandoME.Visible = False
        '        Else
        '            Me.txtCobrandoME.Visible = True
        '        End If
        '    Case Is = "Credito"
        '        If Me.txtCobrandoCredito.Visible Then
        '            Me.txtCobrandoCredito.Visible = False
        '        Else
        '            Me.txtCobrandoCredito.Visible = True
        '        End If
        '    Case Is = "PagoAdeudo"
        '        If Me.txtAdeudoAPagar.Visible Then
        '            Me.txtAdeudoAPagar.Visible = False
        '        Else
        '            Me.txtAdeudoAPagar.Visible = True
        '        End If
        'End Select
    End Sub
#End Region

    Private Sub labelsTxtVisible()
        Me.labelCobrandoEfectivoRecibido.Visible = True
        Me.labelCobrandoME.Visible = True
        Me.labelCobrandoCS.Visible = True
        If _ClienteCreditoUsado > 0 Then
            Me.labelAdeudoAPagar.Visible = True
        End If
    End Sub

#Region "Raisevent"
    Private Sub CerrarEntreMECS()
        'Me.panelCaptura.Controls.Remove(_Cobrando_EntreMECS)
        ''
        'If _Cobrando_EntreMECS.TipoProcesado = 1 Then                           'Monedero Electronico
        '    Me.txtCobrandoME.Text = _Cobrando_EntreMECS.txtImporte.Text
        '    If cLibreriasDatos.ValorDecimal(_Cobrando_EntreMECS.txtImporte.Text) > 0 Then
        '        ME_ImporteCapturado = True
        '        txtCobrandoME.ForeColor = Color.Yellow
        '        txtCobrandoME.BackColor = Color.Red
        '        '
        '        Me.txtCobrandoME.Text = Format(cLibreriasDatos.ValorDecimal(Me.txtCobrandoME.Text), _lFormatoCantidad)
        '        RecalculaImportes()
        '    End If
        '    '
        '    Me.txtCobrandoME.Focus()
        'Else                                                                    'Credito Superventas
        '    Me.txtCobrandoCredito.Text = _Cobrando_EntreMECS.txtImporte.Text
        '    If cLibreriasDatos.ValorDecimal(_Cobrando_EntreMECS.txtImporte.Text) > 0 Then
        '        Credito_ImporteCapturado = True
        '        txtCobrandoCredito.ForeColor = Color.Yellow
        '        txtCobrandoCredito.BackColor = Color.Red
        '        Credito_ImporteCapturado = True
        '        '
        '        Me.txtCobrandoCredito.Text = Format(cLibreriasDatos.ValorDecimal(Me.txtCobrandoCredito.Text), _lFormatoCantidad)
        '        RecalculaImportes()
        '    End If
        '    '
        '    Me.txtCobrandoCredito.Focus()
        'End If
        ''
        'Me.panelCapturaImportes.Visible = True
        'My.Computer.Keyboard.SendKeys("{TAB}", True)
    End Sub
#End Region

End Class
