using System;
using System.Collections.Generic;
using System.Data;
using Datos;

namespace Negocio
{
    [Serializable()]
    public class ControlName
    {
        #region [Atributos]
        private int id;
        private string nombre;
        private string descripcion;
        private string valor;
        #endregion

        #region [Propiedades]
        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Valor { get => valor; set => valor = value; }
        #endregion


        #region [Contructores]
        public ControlName()
        {
            Id = 0;
            Nombre = string.Empty;
            Descripcion = string.Empty;
            Valor = "0";
        }
        public ControlName(int id,string nombre,string descripcion,string valor)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Valor = valor;
        }
        #endregion

        #region [Métodos]
        public string ObtenerControlNames(int citem_id, out List<ControlName> controlNames)
        {
            #region [Obtiene los controlnames disponibles para el servicio]
            BDDTYTAN bddTytan = new BDDTYTAN();
            DataTable datos = new DataTable();
            DataTable datos2 = new DataTable();
            DataTable datos3 = new DataTable();
            string respuesta = string.Empty;
            controlNames = new List<ControlName>();
            string pvalor = string.Empty;
            string pservicio = string.Empty;
            string pserviciohd = string.Empty;
            int i = 0; int calculados = 0;

            if (citem_id > 0) 
            {               
                respuesta = bddTytan.EjecutarConsulta("select (ypkg_instdiferida.fnrevisapropadi(cb.cparty_id, cb.account_id, cb.citem_id, 10, 500767, sysdate, 1)) cblCalificacionCliente, "
                                           + " (cb.costcenter_id)cblCiudad, (cb.paymenttype_id)cblFormaPago,"
                                           + " nvl(ypkg_instdiferida.fnrevisapropadi(cb.cparty_id, cb.account_id, cb.citem_id, 8, 503133, sysdate, 1),"
                                           + " ypkg_instdiferida.fnrevisapropadi(cb.cparty_id, cb.account_id, cb.citem_id, 11, 500060, sysdate, 1))cblNegocio,"
                                           + " (ypkg_instdiferida.fnrevisapropadi(cb.cparty_id, cb.account_id, cb.citem_id, 11, 502271, sysdate, 1)) cblNodo,"
                                           //+ " (decode(cb.tariffplan_id, '1000009', (SELECT ii.NUMBERVALUE FROM twfliprocessparameters ii WHERE ii.iprocess_id = cb.contract_id "
                                           //+ " AND ii.param_id = X_WFLTVC.fngetwflparameterid('RP_VELOCIDAD_GPON')),cb.tariffplanvariant_id))cblPlan, " 
                                           + " cb.tariffplanvariant_id cblPlan, (cb.tariffplan_id)cblTariffPlan,'0' cblTipoContratacion,"
                                           + " cb.accounttype_id cblTipoCuenta, (case when tipodeco = 503950  then decoder when tipodeco = 0 then ordernumber else '0' end) cblTipoServicio,"
                                           + " (case when tipodeco != 503950 and tipodeco != 0  then ordernumber else '0' end)  cblTipoServicioHD,"                                           
                                           + " nvl(ypkg_instdiferida.fnrevisapropadi(cb.cparty_id, cb.account_id, cb.citem_id, 8, 500320, sysdate, 1), 0) cbltipodecodificador,"
                                           + " (cb.product_id)cmbProduct,nvl(ypkg_instdiferida.fnrevisapropadi(cb.cparty_id, cb.account_id, cb.citem_id, 8, 500059, sysdate, 1), 0) rbtTecnologia, "
                                           + " (nvl(ypkg_instdiferida.fnrevisapropadi(cb.cparty_id, cb.account_id, cb.citem_id, 8, 505517, sysdate, 1),'0')) cblPropiedadAdicional,"   //otras operadoras por defecto
                                           + " (nvl(ypkg_instdiferida.fnrevisapropadi(cb.cparty_id, cb.account_id, cb.citem_id, 11, 500065, sysdate, 1),'0')) cblCategoriaCta, '0' cblPremisaAdicional "
                                           + " from(select b.cparty_id, b.account_id, i.citem_id, b.costcenter_id, b.paymenttype_id,"
                                           + " nvl(ypkg_instdiferida.fnrevisapropadi(i.cparty_id, i.cpartyaccount_id, i.citem_id, 8, 500320, sysdate, 1), 0) tipodeco,"
                                           + " nvl(ypkg_instdiferida.fnrevisapropadi(i.cparty_id, i.cpartyaccount_id, i.citem_id, 8, 503047, sysdate, 1), 0) decoder,"
                                           + " to_char(xPromotionTool.fnGetOrderNumber(i.product_id, i.citem_id, 'RC', i.dfrom)) ordernumber,"
                                           + " i.tariffplan_id, i.tariffplanvariant_id, b.accounttype_id, i.product_id,i.contract_id"
                                           + " from tamcpartyaccountd b, tamcontracteditemd i "
                                           + " where  i.citem_id = " + citem_id + " and i.isvalid = 'Y'"
                                           + " and i.cparty_id = b.cparty_id"
                                           + " and i.cpartyaccount_id = b.account_id)cb", out datos2);
                if (datos2.Rows.Count > 0)
                {
                    calculados = 1; 
                    i = 0;
                    respuesta = bddTytan.EjecutarConsulta("select xPromotionTool.fnGetServicioId(nvl(" + datos2.Rows[0]["cblTipoServicioHD"] + "," + datos2.Rows[0]["cblTipoServicio"] + ")) valor from dual", out datos3);
                    if (datos3.Rows.Count > 0)
                    {
                        if (datos2.Rows[0]["cblTipoServicioHD"].ToString() == "0") pservicio = datos3.Rows[0]["valor"].ToString(); else pserviciohd = datos3.Rows[0]["valor"].ToString();                        
                            
                    }

                }
            }


            respuesta = bddTytan.EjecutarConsulta("select (case when a.name ='cblPropiedadAdicional' then 505517  else a.id end)id,a.name,a.description "
            + "from trepvaluelistitems a  where a.valuelist_id = 500152 and a.fvalue = 1 order by to_number(a.evalue)", out datos);
            if (respuesta.ToUpper() == "OK")
            {
                if (datos.Rows.Count > 1)
                {                    
                    foreach (DataRow dr in datos.Rows)
                    {                                         
                        pvalor = "0";
                        if (dr["name"].ToString() == "cblTipoServicioHD" && pserviciohd != string.Empty)
                        {
                            pvalor = pserviciohd;
                        }
                        if (dr["name"].ToString() == "cblTipoServicio" && pservicio != string.Empty)
                        {
                            pvalor = pservicio;
                        }
                        if (calculados == 1 && i < datos2.Columns.Count)
                        {
                            if (pvalor == "0" && datos2.Rows[0][i].ToString() != "") pvalor = datos2.Rows[0][i].ToString();                             
                            i++;
                            //if (i == 7) i = 7;
                        }
                        controlNames.Add(new ControlName(Convert.ToInt32(dr[id].ToString()), dr["name"].ToString(), dr["description"].ToString(), pvalor));
                    }
                    return "Ok";
                }
                return "No existe controlnmaes disponibles";
            }
            else
            {
                return respuesta;
            }


            #endregion
        }
        public override string ToString()
        { return "Id:" + Id + " Nombre:" + Nombre + " Valor:" + Valor;
        }
        #endregion
    }
}
