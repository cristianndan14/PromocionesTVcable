using Dato;
using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PromoComercial
    {
        private int id;
        private string nombre;
        private string descripcionPromo;
        private List<Promocion> promociones;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string DescripcionPromo { get => descripcionPromo; set => descripcionPromo = value; }
        public List<Promocion> Promociones { get => promociones; set => promociones = value; }

        public PromoComercial()
        {
            Id = 0;
            Nombre = string.Empty;
            DescripcionPromo = string.Empty;
            Promociones = new List<Promocion>();
        }

        public string RegitraPromociones(Int32 ProcessId)
        {
            try
            {
                BDDTYTAN bddTytan = new BDDTYTAN();
                DataTable datos = new DataTable();
                DataTable datos1 = new DataTable();
                ProcedimientoAlmacenadoOracle pa = new ProcedimientoAlmacenadoOracle();
                string respuesta = string.Empty;
                string mensajeError=string.Empty;
                string sqlextra = string.Empty;
                Int32 citem_aux = 0;
                


                foreach (Promocion promo in Promociones)
                {
                    citem_aux = 0;
                    ////string v_caption = string.Empty;
                    ////string v_valorpropiedad = "0";
                    if (promo.Citem_id == 0)
                   {
                                               

                        ////if (promo.Controlnames[14].Id.ToString() != "NULL" && promo.Controlnames[14].Valor.ToString() != "NULL" && promo.Tipo == 3)
                        ////{
                        ////    respuesta = bddTytan.EjecutarConsulta(" SELECT a.caption, (case when a.valuelist_id is null then '" + promo.Controlnames[14].Valor + "' "
                        ////    + "else (select name from trepvaluelistitems where id =" + promo.Controlnames[14].Valor + ") end) valorprop "
                        ////    + " FROM trepprops a where propertymeaning_id = " + promo.Controlnames[14].Id + " and rownum = 1 ", out datos1);
                        ////}
                        ////if (datos.Rows.Count > 0)
                        ////{
                        ////    v_caption = datos1.Rows[0]["caption"].ToString();
                        ////    v_valorpropiedad = datos1.Rows[0]["valorprop"].ToString();
                        ////}

                        respuesta = bddTytan.EjecutarConsulta("SELECT * from (select a.cparty_id, a.cpartyaccount_id, a.citem_id, "
                         + "(select to_number(bvalue) from trepvaluelistitems where id = nvl(" + promo.Controlnames[10].Valor + ",nvl(" + promo.Controlnames[9].Valor + ",0))) tiposerv, "
                         + "(CASE when '" + promo.Controlnames[14].Valor + "' = 'NULL' then 0 else " + promo.Controlnames[14].Valor + " END) propadi "
                         + "from tamcontracteditemd a "
                         + "where a.citem_id > 0  and a.contract_id = " + ProcessId + " and a.contractedto is null and a.isvalid = 'Y' "
                         + "and a.product_id = " + promo.Controlnames[12].Valor + " "
                         + "and (CASE when '" + promo.Controlnames[5].Valor + "' = 'NULL' then 0 else "
                         + "(decode(a.tariffplan_id, '1000009', (SELECT ii.NUMBERVALUE FROM twfliprocessparameters ii WHERE ii.iprocess_id = a.contract_id "
                         + " and ii.param_id = X_WFLTVC.fngetwflparameterid('RP_VELOCIDAD_GPON')),a.tariffplanvariant_id)) END)= "
                         + "(CASE when '" + promo.Controlnames[5].Valor + "' = 'NULL' then 0 else " + promo.Controlnames[5].Valor + " END) "
                         + "and (CASE when '" + promo.Controlnames[11].Valor + "'='NULL' then 0 else " + promo.Controlnames[11].Valor + "  END) ="
                         + "(CASE when '" + promo.Controlnames[11].Valor + "'='NULL' then 0 else to_number(nvl(ypkg_instdiferida.fnrevisapropadi(a.cparty_id, a.cpartyaccount_id, a.citem_id, 8, 500320, sysdate, 1), 0)) END)) tb "
                         + "WHERE tb.propadi = (CASE WHEN tb.propadi =0 THEN tb.propadi ELSE "
                         + "TO_NUMBER(nvl(ypkg_instdiferida.fnrevisapropadi(tb.cparty_id, tb.cpartyaccount_id, tb.citem_id, NULL, nvl(" + promo.Controlnames[14].Id + ",0), sysdate, 1),0)) END) "
                         + "TB.tiposerv = (CASE WHEN tb.propadi =0 THEN tb.tiposerv ELSE  ", out datos);



                        ////+ "WHERE tb.propadi = (case when " + promo.Tipo + " < 3 then "
                        ////+ "to_number(nvl(ypkg_instdiferida.fnrevisapropadi(tb.cparty_id, tb.cpartyaccount_id, tb.citem_id, NULL, " + promo.Controlnames[14].Id + ", sysdate, 1),0)) "
                        ////+ "else ((case when substr(pa.aditional_properties,1,(instr(pa.aditional_properties,'|')-1)) = '" + v_caption + "' then "
                        ////       + "(case when instr(substr(pa.aditional_properties,(instr(pa.aditional_properties,'|')+1),length(pa.aditional_properties)), ('," + v_valorpropiedad + ",')) > 0 then 1 "
                        ////       + "else 0 end) else 0 end) else 1 end))  end)", out datos);



                        ////+ "and nvl(" + promo.Controlnames[14].Valor + ",0)   = (case when "+ promo.Tipo +" < 3 then " 
                        ////+ "(nvl(ypkg_instdiferida.fnrevisapropadi(a.cparty_id, a.cpartyaccount_id, a.citem_id, NULL, " + promo.Controlnames[14].Id +", sysdate, 1),0)) else (select 1 from dual)  end)", out datos);


                        ////+"and to_number(case when " + promo.Controlnames[12].Valor + " = 153 THEN to_number(xPromotionTool.fnGetOrderNumber(" + promo.Controlnames[12].Valor + ", a.citem_id, 'RC',a.dfrom) )"
                        ////                      + "else to_number(nvl(ypkg_instdiferida.fnrevisapropadi(a.cparty_id, a.cpartyaccount_id, a.citem_id, 8, 503047, sysdate, 0), 0)) end) = "
                        ////                      + "(select to_number(bvalue) from trepvaluelistitems where id = nvl(" + promo.Controlnames[10].Valor + ",nvl(" + promo.Controlnames[9].Valor + ",0)))"
                        if (respuesta.ToUpper() == "OK")
                         {
                            citem_aux = Convert.ToInt32(datos.Rows[0]["citem_id"]);
                         }
                         
                    }
                    else
                    {
                        citem_aux = promo.Citem_id;
                    }
                    pa.ComandText = "YPKG_APLICA_PROMOCION.REGISTRA_PROMOCIONES";
                    pa.Parameters.Add(new ParametroOracle("PN_IDPROMO",Oracle.ManagedDataAccess.Client.OracleDbType.Int32,promo.Id.ToString()));
                    pa.Parameters.Add(new ParametroOracle("PN_TIPO", Oracle.ManagedDataAccess.Client.OracleDbType.Int32, promo.Tipo.ToString()));
                    pa.Parameters.Add(new ParametroOracle("PN_CITEM", Oracle.ManagedDataAccess.Client.OracleDbType.Int32, citem_aux.ToString()));
                    pa.Parameters.Add(new ParametroOracle("PD_FECHA", Oracle.ManagedDataAccess.Client.OracleDbType.Date, null));
                    pa.Parameters.Add(new ParametroOracle("PN_PROCESSID", Oracle.ManagedDataAccess.Client.OracleDbType.Int32, ProcessId.ToString()));

                    respuesta = bddTytan.EjecutarProcedimientoAlmacenadoOracle(pa);
                    pa.Parameters.Clear();
                    if (respuesta.ToUpper() != "OK")
                        mensajeError = "Error al registrar en la promo Id=" + promo.Id + " para el Citemid=" + promo.Citem_id;

                }
                if (mensajeError != string.Empty)
                    return mensajeError;
                else
                    return "Ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
