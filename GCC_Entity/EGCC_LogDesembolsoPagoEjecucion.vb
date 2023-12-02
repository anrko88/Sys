Imports System.Reflection
Imports System.Collections.Generic
Imports System.Xml.Serialization

''' <summary>
''' Clase que permite definir los atributos y propiedades de la entidad EGCC_LogDesembolsoPagoEjecucion
''' </summary>
''' <remarks>
''' Creado Por         : IBK - AAE
''' Fecha de Creación  : 06/11/2012
''' </remarks>
<Serializable(), XmlRoot("EGCC_LogDesembolsoPagoEjecucion")> _
Public Class EGCC_LogDesembolsoPagoEjecucion
#Region " Atributos "

    Private _strCodSolicitudCredito As String
    Private _strCodInstruccionDesembolso As String
    Private _strCodAgrupacion As String
    Private _strCodProveedor As String
    Private _strCodMonedaAgrupacion As String
    Private _strFechaHora As String
    Private _strFCDCODRET As String
    Private _strFCDCODTRAN As String
    Private _strFCDPROGRAMA As String
    Private _strFCDUSUARIO As String
    Private _strFCDNUDOC As String
    Private _strFCDTRANCODE As String
    Private _strFCDFECPROSS As String
    Private _strFCDFECPROYY As String
    Private _strFCDFECPROMM As String
    Private _strFCDFECPRODD As String
    Private _strFCDREGEMPLEADO As String
    Private _strFCDTIENDAORIGEN As String
    Private _strFCDTIPCTACR As String
    Private _strFCDCRCTL1 As String
    Private _strFCDCRCTL2 As String
    Private _strFCDCRCTL3 As String
    Private _strFCDCRCTL4 As String
    Private _strFCDCRACCT As String
    Private _strFCDCRFILL As String
    Private _strFCDTIPCTADB As String
    Private _strFCDDBCTL1 As String
    Private _strFCDDBCTL2 As String
    Private _strFCDDBCTL3 As String
    Private _strFCDDBCTL4 As String
    Private _strFCDDBACCT As String
    Private _strFCDDBFILL As String
    Private _strFCDEXTORNO As String
    Private _strFCDSHORTDESC As String
    Private _strFCDAMOUNTCR As String
    Private _strFCDAMOUNTDB As String
    Private _strFCDCOBROFORZOSO As String
    Private _strFCDCOBROPARCIAL As String
    Private _strFCDCODUNI As String
    Private _strFCDCTAFLGOC As String
    Private _strFCDCTAMONCF As String
    Private _strFCDCTACLATC As String
    Private _strFCDCTATCCF As String
    Private _strFCDCTAIMPEQUIV As String
    Private _strFILLERINP As String
    Private _strFCDLENGTHCOMMAREA As String
    Private _strFCDCODRETTOLD As String
    Private _strFCDCODRETO As String
    Private _strFCDMSGERROR As String
    Private _strFCDFC04NRODOCUMENTO As String
    Private _strFCDFC04IMPORTEDESEMB As String
    Private _strFCDFC04FLGEXTORNO As String
    Private _strFILLEROUT As String
    Private _strAudFechaRegistro As String
    Private _strAudFechaModificacion As String
    Private _strAudUsuarioRegistro As String
    Private _strAudUsuarioModificacion As String
    Private _strCodRetorno As String
    Private _strResultado As String

#End Region


#Region " Propiedades "

    <XmlElement("CodSolicitudCredito")> _
    Public Property CodSolicitudCredito() As String
        Get
            Return Me._strCodSolicitudCredito
        End Get
        Set(ByVal value As String)
            Me._strCodSolicitudCredito = value
        End Set
    End Property

    <XmlElement("CodInstruccionDesembolso")> _
    Public Property CodInstruccionDesembolso() As String
        Get
            Return Me._strCodInstruccionDesembolso
        End Get
        Set(ByVal value As String)
            Me._strCodInstruccionDesembolso = value
        End Set
    End Property
    <XmlElement("CodAgrupacion")> _
    Public Property CodAgrupacion() As String
        Get
            Return Me._strCodAgrupacion
        End Get
        Set(ByVal value As String)
            Me._strCodAgrupacion = value
        End Set
    End Property
    <XmlElement("CodProveedor")> _
    Public Property CodProveedor() As String
        Get
            Return Me._strCodProveedor
        End Get
        Set(ByVal value As String)
            Me._strCodProveedor = value
        End Set
    End Property
    <XmlElement("CodMonedaAgrupacion")> _
    Public Property CodMonedaAgrupacion() As String
        Get
            Return Me._strCodMonedaAgrupacion
        End Get
        Set(ByVal value As String)
            Me._strCodMonedaAgrupacion = value
        End Set
    End Property
    <XmlElement("FechaHora")> _
    Public Property FechaHora() As String
        Get
            Return Me._strFechaHora
        End Get
        Set(ByVal value As String)
            Me._strFechaHora = value
        End Set
    End Property
    <XmlElement("FCDCODRET")> _
    Public Property FCDCODRET() As String
        Get
            Return Me._strFCDCODRET
        End Get
        Set(ByVal value As String)
            Me._strFCDCODRET = value
        End Set
    End Property
    <XmlElement("FCDCODTRAN")> _
    Public Property FCDCODTRAN() As String
        Get
            Return Me._strFCDCODTRAN
        End Get
        Set(ByVal value As String)
            Me._strFCDCODTRAN = value
        End Set
    End Property
    <XmlElement("FCDPROGRAMA")> _
    Public Property FCDPROGRAMA() As String
        Get
            Return Me._strFCDPROGRAMA
        End Get
        Set(ByVal value As String)
            Me._strFCDPROGRAMA = value
        End Set
    End Property
    <XmlElement("FCDUSUARIO")> _
    Public Property FCDUSUARIO() As String
        Get
            Return Me._strFCDUSUARIO
        End Get
        Set(ByVal value As String)
            Me._strFCDUSUARIO = value
        End Set
    End Property
    <XmlElement("FCDNUDOC")> _
    Public Property FCDNUDOC() As String
        Get
            Return Me._strFCDNUDOC
        End Get
        Set(ByVal value As String)
            Me._strFCDNUDOC = value
        End Set
    End Property
    <XmlElement("FCDTRANCODE")> _
    Public Property FCDTRANCODE() As String
        Get
            Return Me._strFCDTRANCODE
        End Get
        Set(ByVal value As String)
            Me._strFCDTRANCODE = value
        End Set
    End Property
    <XmlElement("FCDFECPROSS")> _
    Public Property FCDFECPROSS() As String
        Get
            Return Me._strFCDFECPROSS
        End Get
        Set(ByVal value As String)
            Me._strFCDFECPROSS = value
        End Set
    End Property
    <XmlElement("FCDFECPROYY")> _
    Public Property FCDFECPROYY() As String
        Get
            Return Me._strFCDFECPROYY
        End Get
        Set(ByVal value As String)
            Me._strFCDFECPROYY = value
        End Set
    End Property
    <XmlElement("FCDFECPROMM")> _
    Public Property FCDFECPROMM() As String
        Get
            Return Me._strFCDFECPROMM
        End Get
        Set(ByVal value As String)
            Me._strFCDFECPROMM = value
        End Set
    End Property
    <XmlElement("FCDFECPRODD")> _
    Public Property FCDFECPRODD() As String
        Get
            Return Me._strFCDFECPRODD
        End Get
        Set(ByVal value As String)
            Me._strFCDFECPRODD = value
        End Set
    End Property
    <XmlElement("FCDREGEMPLEADO")> _
    Public Property FCDREGEMPLEADO() As String
        Get
            Return Me._strFCDREGEMPLEADO
        End Get
        Set(ByVal value As String)
            Me._strFCDREGEMPLEADO = value
        End Set
    End Property
    <XmlElement("FCDTIENDAORIGEN")> _
    Public Property FCDTIENDAORIGEN() As String
        Get
            Return Me._strFCDTIENDAORIGEN
        End Get
        Set(ByVal value As String)
            Me._strFCDTIENDAORIGEN = value
        End Set
    End Property
    <XmlElement("FCDTIPCTACR")> _
    Public Property FCDTIPCTACR() As String
        Get
            Return Me._strFCDTIPCTACR
        End Get
        Set(ByVal value As String)
            Me._strFCDTIPCTACR = value
        End Set
    End Property
    <XmlElement("FCDCRCTL1")> _
    Public Property FCDCRCTL1() As String
        Get
            Return Me._strFCDCRCTL1
        End Get
        Set(ByVal value As String)
            Me._strFCDCRCTL1 = value
        End Set
    End Property
    <XmlElement("FCDCRCTL2")> _
    Public Property FCDCRCTL2() As String
        Get
            Return Me._strFCDCRCTL2
        End Get
        Set(ByVal value As String)
            Me._strFCDCRCTL2 = value
        End Set
    End Property
    <XmlElement("FCDCRCTL3")> _
    Public Property FCDCRCTL3() As String
        Get
            Return Me._strFCDCRCTL3
        End Get
        Set(ByVal value As String)
            Me._strFCDCRCTL3 = value
        End Set
    End Property
    <XmlElement("FCDCRCTL4")> _
    Public Property FCDCRCTL4() As String
        Get
            Return Me._strFCDCRCTL4
        End Get
        Set(ByVal value As String)
            Me._strFCDCRCTL4 = value
        End Set
    End Property
    <XmlElement("FCDCRACCT")> _
    Public Property FCDCRACCT() As String
        Get
            Return Me._strFCDCRACCT
        End Get
        Set(ByVal value As String)
            Me._strFCDCRACCT = value
        End Set
    End Property
    <XmlElement("FCDCRFILL")> _
    Public Property FCDCRFILL() As String
        Get
            Return Me._strFCDCRFILL
        End Get
        Set(ByVal value As String)
            Me._strFCDCRFILL = value
        End Set
    End Property
    <XmlElement("FCDTIPCTADB")> _
    Public Property FCDTIPCTADB() As String
        Get
            Return Me._strFCDTIPCTADB
        End Get
        Set(ByVal value As String)
            Me._strFCDTIPCTADB = value
        End Set
    End Property
    <XmlElement("FCDDBCTL1")> _
    Public Property FCDDBCTL1() As String
        Get
            Return Me._strFCDDBCTL1
        End Get
        Set(ByVal value As String)
            Me._strFCDDBCTL1 = value
        End Set
    End Property
    <XmlElement("FCDDBCTL2")> _
    Public Property FCDDBCTL2() As String
        Get
            Return Me._strFCDDBCTL2
        End Get
        Set(ByVal value As String)
            Me._strFCDDBCTL2 = value
        End Set
    End Property
    <XmlElement("FCDDBCTL3")> _
    Public Property FCDDBCTL3() As String
        Get
            Return Me._strFCDDBCTL3
        End Get
        Set(ByVal value As String)
            Me._strFCDDBCTL3 = value
        End Set
    End Property
    <XmlElement("FCDDBCTL4")> _
    Public Property FCDDBCTL4() As String
        Get
            Return Me._strFCDDBCTL4
        End Get
        Set(ByVal value As String)
            Me._strFCDDBCTL4 = value
        End Set
    End Property
    <XmlElement("FCDDBACCT")> _
    Public Property FCDDBACCT() As String
        Get
            Return Me._strFCDDBACCT
        End Get
        Set(ByVal value As String)
            Me._strFCDDBACCT = value
        End Set
    End Property
    <XmlElement("FCDDBFILL")> _
    Public Property FCDDBFILL() As String
        Get
            Return Me._strFCDDBFILL
        End Get
        Set(ByVal value As String)
            Me._strFCDDBFILL = value
        End Set
    End Property
    <XmlElement("FCDEXTORNO")> _
    Public Property FCDEXTORNO() As String
        Get
            Return Me._strFCDEXTORNO
        End Get
        Set(ByVal value As String)
            Me._strFCDEXTORNO = value
        End Set
    End Property
    <XmlElement("FCDSHORTDESC")> _
    Public Property FCDSHORTDESC() As String
        Get
            Return Me._strFCDSHORTDESC
        End Get
        Set(ByVal value As String)
            Me._strFCDSHORTDESC = value
        End Set
    End Property
    <XmlElement("FCDAMOUNTCR")> _
    Public Property FCDAMOUNTCR() As String
        Get
            Return Me._strFCDAMOUNTCR
        End Get
        Set(ByVal value As String)
            Me._strFCDAMOUNTCR = value
        End Set
    End Property
    <XmlElement("FCDAMOUNTDB")> _
    Public Property FCDAMOUNTDB() As String
        Get
            Return Me._strFCDAMOUNTDB
        End Get
        Set(ByVal value As String)
            Me._strFCDAMOUNTDB = value
        End Set
    End Property
    <XmlElement("FCDCOBROFORZOSO")> _
    Public Property FCDCOBROFORZOSO() As String
        Get
            Return Me._strFCDCOBROFORZOSO
        End Get
        Set(ByVal value As String)
            Me._strFCDCOBROFORZOSO = value
        End Set
    End Property
    <XmlElement("FCDCOBROPARCIAL")> _
    Public Property FCDCOBROPARCIAL() As String
        Get
            Return Me._strFCDCOBROPARCIAL
        End Get
        Set(ByVal value As String)
            Me._strFCDCOBROPARCIAL = value
        End Set
    End Property
    <XmlElement("FCDCODUNI")> _
    Public Property FCDCODUNI() As String
        Get
            Return Me._strFCDCODUNI
        End Get
        Set(ByVal value As String)
            Me._strFCDCODUNI = value
        End Set
    End Property
    <XmlElement("FCDCTAFLGOC")> _
    Public Property FCDCTAFLGOC() As String
        Get
            Return Me._strFCDCTAFLGOC
        End Get
        Set(ByVal value As String)
            Me._strFCDCTAFLGOC = value
        End Set
    End Property
    <XmlElement("FCDCTAMONCF")> _
    Public Property FCDCTAMONCF() As String
        Get
            Return Me._strFCDCTAMONCF
        End Get
        Set(ByVal value As String)
            Me._strFCDCTAMONCF = value
        End Set
    End Property
    <XmlElement("FCDCTACLATC")> _
    Public Property FCDCTACLATC() As String
        Get
            Return Me._strFCDCTACLATC
        End Get
        Set(ByVal value As String)
            Me._strFCDCTACLATC = value
        End Set
    End Property
    <XmlElement("FCDCTATCCF")> _
    Public Property FCDCTATCCF() As String
        Get
            Return Me._strFCDCTATCCF
        End Get
        Set(ByVal value As String)
            Me._strFCDCTATCCF = value
        End Set
    End Property
    <XmlElement("FCDCTAIMPEQUIV")> _
    Public Property FCDCTAIMPEQUIV() As String
        Get
            Return Me._strFCDCTAIMPEQUIV
        End Get
        Set(ByVal value As String)
            Me._strFCDCTAIMPEQUIV = value
        End Set
    End Property
    <XmlElement("FILLERINP")> _
    Public Property FILLERINP() As String
        Get
            Return Me._strFILLERINP
        End Get
        Set(ByVal value As String)
            Me._strFILLERINP = value
        End Set
    End Property
    <XmlElement("FCDLENGTHCOMMAREA")> _
    Public Property FCDLENGTHCOMMAREA() As String
        Get
            Return Me._strFCDLENGTHCOMMAREA
        End Get
        Set(ByVal value As String)
            Me._strFCDLENGTHCOMMAREA = value
        End Set
    End Property
    <XmlElement("FCDCODRETTOLD")> _
    Public Property FCDCODRETTOLD() As String
        Get
            Return Me._strFCDCODRETTOLD
        End Get
        Set(ByVal value As String)
            Me._strFCDCODRETTOLD = value
        End Set
    End Property
    <XmlElement("FCDCODRETO")> _
    Public Property FCDCODRETO() As String
        Get
            Return Me._strFCDCODRETO
        End Get
        Set(ByVal value As String)
            Me._strFCDCODRETO = value
        End Set
    End Property
    <XmlElement("FCDMSGERROR")> _
    Public Property FCDMSGERROR() As String
        Get
            Return Me._strFCDMSGERROR
        End Get
        Set(ByVal value As String)
            Me._strFCDMSGERROR = value
        End Set
    End Property
    <XmlElement("FCDFC04NRODOCUMENTO")> _
    Public Property FCDFC04NRODOCUMENTO() As String
        Get
            Return Me._strFCDFC04NRODOCUMENTO
        End Get
        Set(ByVal value As String)
            Me._strFCDFC04NRODOCUMENTO = value
        End Set
    End Property
    <XmlElement("FCDFC04IMPORTEDESEMB")> _
    Public Property FCDFC04IMPORTEDESEMB() As String
        Get
            Return Me._strFCDFC04IMPORTEDESEMB
        End Get
        Set(ByVal value As String)
            Me._strFCDFC04IMPORTEDESEMB = value
        End Set
    End Property
    <XmlElement("FCDFC04FLGEXTORNO")> _
    Public Property FCDFC04FLGEXTORNO() As String
        Get
            Return Me._strFCDFC04FLGEXTORNO
        End Get
        Set(ByVal value As String)
            Me._strFCDFC04FLGEXTORNO = value
        End Set
    End Property
    <XmlElement("FILLEROUT")> _
    Public Property FILLEROUT() As String
        Get
            Return Me._strFILLEROUT
        End Get
        Set(ByVal value As String)
            Me._strFILLEROUT = value
        End Set
    End Property
    <XmlElement("AudFechaRegistro")> _
    Public Property AudFechaRegistro() As String
        Get
            Return Me._strAudFechaRegistro
        End Get
        Set(ByVal value As String)
            Me._strAudFechaRegistro = value
        End Set
    End Property
    <XmlElement("AudFechaModificacion")> _
    Public Property AudFechaModificacion() As String
        Get
            Return Me._strAudFechaModificacion
        End Get
        Set(ByVal value As String)
            Me._strAudFechaModificacion = value
        End Set
    End Property
    <XmlElement("AudUsuarioRegistro")> _
    Public Property AudUsuarioRegistro() As String
        Get
            Return Me._strAudUsuarioRegistro
        End Get
        Set(ByVal value As String)
            Me._strAudUsuarioRegistro = value
        End Set
    End Property
    <XmlElement("AudUsuarioModificacion")> _
    Public Property AudUsuarioModificacion() As String
        Get
            Return Me._strAudUsuarioModificacion
        End Get
        Set(ByVal value As String)
            Me._strAudUsuarioModificacion = value
        End Set
    End Property
    <XmlElement("CodRetorno")> _
    Public Property CodRetorno() As String
        Get
            Return Me._strCodRetorno
        End Get
        Set(ByVal value As String)
            Me._strCodRetorno = value
        End Set
    End Property
    <XmlElement("Resultado")> _
    Public Property Resultado() As String
        Get
            Return Me._strResultado
        End Get
        Set(ByVal value As String)
            Me._strResultado = value
        End Set
    End Property

#End Region
End Class
