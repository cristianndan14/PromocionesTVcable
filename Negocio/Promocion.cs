using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Serialization;
using Datos;

namespace Negocio
{
    [Serializable()]
    [XmlInclude(typeof(DetallePromocion))]

    public class Promocion
    {
        #region [Atributos]
        private Int32 id;
        private Int32 tipo;
        private Int32 citem_id;
        private string descripcion;
        private DateTime startPromo;
        private DateTime endPromo;
        private string valorBase;
        private string valorProducto;
        private string descripPromo;
        private List<DetallePromocion> detallesPromocion;
        private List<ControlName> controlnames;
        #endregion

        #region [Propiedades]
        public Int32 Id { get => id; set => id = value; }
        public Int32 Tipo { get => tipo; set => tipo = value; }
        public Int32 Citem_id { get => citem_id; set => citem_id = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public DateTime StartPromo { get => startPromo; set => startPromo = value; }
        public DateTime EndPromo { get => endPromo; set => endPromo = value; }
        public string ValorBase { get => valorBase; set => valorBase = value; }
        public string ValorProducto { get => valorProducto; set => valorProducto = value; }
        public string DescripPromo { get => descripPromo; set => descripPromo = value; }
        public List<DetallePromocion> DetallesPromocion { get => detallesPromocion; set => detallesPromocion = value; }
        public List<ControlName> Controlnames { get => controlnames; set => controlnames = value; }
        #endregion

        #region [Constructores]
        public Promocion()
        {
            Id = 0;
            Tipo = 0;
            Citem_id = 0;
            Descripcion = string.Empty;
            StartPromo = DateTime.Now;
            EndPromo = DateTime.Now;
            ValorBase = string.Empty;
            ValorProducto = string.Empty;
            DescripPromo = string.Empty;            
            DetallesPromocion = new List<DetallePromocion>();
            Controlnames = new List<ControlName>();
        }

        public Promocion(Int32 id, Int32 tipo, Int32 citem_id, string descripcion, DateTime startPromo, DateTime endpromo, string valorbase, string valorProducto, string descripPromo, List<ControlName> controlnames, List<DetallePromocion> detallesPromocion)
        {
            Id = id;
            Tipo = tipo;
            Citem_id = citem_id;
            Descripcion = descripcion;
            StartPromo = startPromo;
            EndPromo = endpromo;
            ValorBase = valorbase;
            ValorProducto = valorProducto;
            DescripPromo = descripPromo;
            DetallesPromocion = detallesPromocion;
            Controlnames = controlnames;
        }
        #endregion

        #region [Métodos]
        public void ObtenerDetallePromo()
        {
            #region [Obtiene el detalle de una promoción]
            BDDTYTAN bddTytan = new BDDTYTAN();
            DataTable datos = new DataTable();
            string bmeses = string.Empty;

            if (tipo ==1 || tipo==2)
            { 
                bddTytan.EjecutarConsulta("select id,controlname, nvl(numbervalue, charvalue) as valor from Y_CATALOGPROMOTIONSITEMS T2 where 1 = 1 and catalogpromotions_id = " + Id + " and controlname in ('cblMesesPromocion', 'txtServicio','txtInstalacion') order by controlname desc,to_number(valor) ", out datos);


                foreach (DataRow dr in datos.Rows)
                {
                    if (dr["controlname"].ToString() == "cblMesesPromocion")
                    {
                        bmeses = bmeses + dr["valor"].ToString() + ",";
                    }
                    else
                    {
                        DetallesPromocion.Add(new DetallePromocion(Convert.ToInt32(dr["id"]), dr["controlname"].ToString(), dr["valor"].ToString()));
                    }
                }
        
                if (bmeses == "" && tipo== 2 ) bmeses = "Por Siempre";

                DetallesPromocion.Add(new DetallePromocion(Convert.ToInt32(1), "cblMesesPromocion", bmeses));
            }
            else
            {
                bddTytan.EjecutarConsulta(" select a.tariffplannew_id, a.tariffplanvariantnew_id, packagetype_id from ytpromocionesadmin a where a.id =" + Id   , out datos);

                foreach (DataRow dr in datos.Rows)
                {
                 DetallesPromocion.Add(new DetallePromocion(Convert.ToInt32(Id), "Upgrade a", dr["tariffplanvariantnew_id"].ToString()+" "+ dr["packagetype_id"].ToString()));
                }

            }

            #endregion
        }


        public string ObtenerPromociones(Contrato contrato, Aplicacion aplicaion, out List<PromoComercial> promosComerciales)
        {
            #region [Obtiene las promociones que aplica para productos contrados]
            //listaProductos = new List<PromoComercial>();
            promosComerciales = new List<PromoComercial>();

            try
            {
                BDDTYTAN bddTytan = new BDDTYTAN();
                DataTable datoscb = new DataTable();
                DataTable datos = new DataTable();
                DataTable datosCtrlName = new DataTable();
                DataTable datosProd = new DataTable();
                DataTable datos2 = new DataTable();
                DataTable datos3 = new DataTable();
                string respuesta = string.Empty, prodName = string.Empty;
                string idstr = string.Empty;
                string valorProd = string.Empty;
                string tipoServAdic = string.Empty;
                int numDecoder=0, aplicaPromo = 0, tipoPromo = 0;
                PromoComercial promocom = new PromoComercial();
                Promocion promo = new Promocion();
                Producto prod = new Producto();
                string meses = string.Empty;
                string porcentaje = "", tipoServ = "", descriPromo = "";
                Double totalTmp, valorBase;
                List<string> listMeses = new List<string>();


                List<ControlName> controlnamesOriginales = new List<ControlName>();
                ControlName cn = new ControlName();
                //cn.ObtenerControlNames(out controlnamesOriginales);

                foreach (Cuenta cuenta in contrato.Cuentas)
                {

                    foreach (Citem prepararCN in cuenta.Citems)
                    {

                        if (prepararCN.Citem_id != "" && (Convert.ToInt32(prepararCN.Citem_id) > 0))
                        {
                            cn.ObtenerControlNames(Convert.ToInt32(prepararCN.Citem_id), out controlnamesOriginales);
                        }
                        else
                        {
                            cn.ObtenerControlNames(0, out controlnamesOriginales);
                        }
                        

                        if (prepararCN.Controlnames != null)
                        {
                            foreach (ControlName aux in prepararCN.Controlnames)
                            {
                                var elementIndex = controlnamesOriginales.FindIndex(i => i.Nombre == aux.Nombre);
                                if (elementIndex > -1)
                                {
                                    //if (aux.Nombre == "cblTipoContratacion") Console.WriteLine("1");

                                        if ((controlnamesOriginales[elementIndex].Valor.ToUpper() == "0" && aux.Valor.ToUpper() != "0") || (aux.Nombre == "cblPropiedadAdicional" && aux.Id != 505517 && aux.Valor.ToUpper() != "0" ))
                                    {                                        
                                        if (aux.Nombre == "cblPropiedadAdicional") controlnamesOriginales[elementIndex].Id = aux.Id;
                                        controlnamesOriginales[elementIndex].Valor = aux.Valor;
                                     
                                    }
                                }
                            }
                        }
                        prepararCN.Controlnames = new List<ControlName>();
                        foreach (ControlName copiarCn in controlnamesOriginales)
                        {
                            prepararCN.Controlnames.Add(new ControlName(copiarCn.Id, copiarCn.Nombre, copiarCn.Descripcion, copiarCn.Valor));
                            copiarCn.Valor = "0";
                        }
                    }

                    var s = Newtonsoft.Json.JsonConvert.SerializeObject(cuenta);


                    respuesta = bddTytan.EjecutarConsulta("select *  from   YTBL_PROMOCOMERCIAL pc "
                                      + "where 1 = 1 "
                                      + "and sysdate BETWEEN pc.dfrom and nvl(pc.dto, sysdate) and isvalid ='Y' "
                                      //+ "and id in(17)"
                                      + "and instr((','||pc.aplicativo_id||','), ('," + aplicaion.Id + ",')) > 0 ORDER BY id desc", out datoscb);
                    if (respuesta.ToUpper() == "OK")
                    {                        
                        foreach (DataRow dr in datoscb.Rows)
                        {
                            promocom = new PromoComercial();
                            string citemaux = string.Empty;

                            foreach (Citem citem in cuenta.Citems)  //nueva estructura
                            {

                                string tipopromo = string.Empty;
                                string nomprod = string.Empty;
                                Int32 pcitem_id = 0;

                                if (citem.Citem_id != "") pcitem_id = Convert.ToInt32(citem.Citem_id);

                                respuesta = bddTytan.EjecutarConsulta("Select (case when " + citem.Controlnames[12].Valor + " is not null then "
                                            + "(select name from tpcproducts where id=" + citem.Controlnames[12].Valor + ") end) nomprod from dual", out datos3);


                                if (respuesta.ToUpper() == "OK") nomprod = datos3.Rows[0]["nomprod"].ToString();

                                string query2 = "SELECT pc.id idgrupopromo, pc.descripcion desgrupopromo,ABS(gr.Promotion_Id) Promotion_Id,pc.aplicativo_id aplicativoconfig,pc.tipo_promo tipopromoconfig,"
                                + "('" + nomprod + "'||(case when gr.tiposerviciohd_id IS NOT NULL OR gr.tiposervicio_id IS NOT NULL then ' '||"
                                + "(select Name from trepvaluelistitems where valuelist_id = 500151 and id = nvl(" + citem.Controlnames[10].Valor + "," + citem.Controlnames[9].Valor + ")) end))Promotion_Code,"
                                + "gr.StartPromo, gr.EndPromo, gr.DFrom, gr.DTo, PromotionMonth, ExtraPropMeaning_id, ExtraProplValue, PromotionPriority,"
                                + " (case when (select count(id) from y_catalogpromotionsitems pi where pi.catalogpromotions_id = ABS(gr.Promotion_Id) and pi.controlname ='txtServicio') >0 then 2  else 1 end) tipo_promo,"
                                + "('['||"
                                + " substr((','||pc.tipo_promo||','),(instr((','||pc.tipo_promo||','),',',1,(REGEXP_COUNT((substr(','||pc.aplicativo_id||',',1,(instr(','||pc.aplicativo_id||',',',' ||" + aplicaion.Id + "|| ',')))),',')))+1), "
                                + " (instr((',' || pc.tipo_promo || ','), ',', 1, (REGEXP_COUNT((substr(',' || pc.aplicativo_id || ',', 1, (instr(',' || pc.aplicativo_id || ',', ',' ||" + aplicaion.Id + "|| ',')))), ',') + 1)) "
                                + "  - instr((',' || pc.tipo_promo || ','), ',', 1, (REGEXP_COUNT((substr(',' || pc.aplicativo_id || ',', 1, (instr(',' || pc.aplicativo_id || ',',',' ||" + aplicaion.Id + "|| ',')))), ','))) - 1 ) )"
                                + "||'[') tipo_promo_apli,"
                                + "(case when gr.extrapropmeaning_id > 0  then "
                                + "(case when gr.extrapropmeaning_id =" + citem.Controlnames[14].Id + " then "
                                + "(case when instr(gr.extraproplvalue, ',' ||" + citem.Controlnames[14].Valor + "|| ',') > 0 then 1  else 0 end)"
                                + "else 0 end)"
                                + "else 1 end)pareado "
                                + " FROM xPromotionPremises gr,YTBL_PROMOCOMERCIAL pc"
                                + " WHERE pc.id = " + dr["ID"] + ""
                                + " and instr((','||pc.promo_id||','),(','|| abs(gr.promotion_id)||',')) >0  and instr((',' || pc.aplicativo_id || ','), ('," + aplicaion.Id + ",')) > 0"
                                + " AND (gr.tipoaccount_id IS NULL OR gr.tipoaccount_id LIKE '%,' ||" + (citem.Controlnames[8].Valor).ToString() + "|| ',%')"
                                + " AND (gr.tariffplan_id IS NULL OR gr.tariffplan_id LIKE '%,' || " + (citem.Controlnames[5].Valor).ToString() + "|| ',%')"
                                + " AND (gr.product_id IS NULL OR gr.product_id LIKE '%,' ||" + (citem.Controlnames[12].Valor).ToString() + " || ',%')"
                                + " AND (gr.formapago_id IS NULL OR gr.formapago_id LIKE '%,' ||" + (citem.Controlnames[2].Valor).ToString() + " || ',%')"
                                + " AND (gr.negocio_id IS NULL OR gr.negocio_id LIKE '%,' ||" + (citem.Controlnames[3].Valor).ToString() + "|| ',%')"
                                + " AND (gr.nodo_id IS NULL OR gr.nodo_id LIKE '%,' ||" + (citem.Controlnames[4].Valor).ToString() + "|| ',%')"
                                + " AND (gr.ciudad_id IS NULL OR gr.ciudad_id LIKE '%,' ||" + (citem.Controlnames[1].Valor).ToString() + "|| ',%')"
                                + " AND(gr.tiposerviciohd_id IS NULL OR gr.tiposerviciohd_id LIKE '%,' ||" + (citem.Controlnames[10].Valor).ToString() + "|| ',%')"
                                + " AND(gr.tiposervicio_id IS NULL OR gr.tiposervicio_id LIKE '%,' ||" + (citem.Controlnames[9].Valor).ToString() + "|| ',%')"
                                + " AND (gr.clficacioncliente_id IS NULL OR gr.clficacioncliente_id LIKE '%,' ||" + (citem.Controlnames[0].Valor).ToString() + "|| ',%')"
                                + " AND (gr.tipocontratacion_id IS NULL OR gr.tipocontratacion_id LIKE '%,' ||" + (citem.Controlnames[7].Valor).ToString() + "|| ',%')"
                                + " AND(gr.tipodecodificador_id IS NULL OR gr.tipodecodificador_id LIKE '%,' ||" + (citem.Controlnames[11].Valor).ToString() + "|| ',%')"
                                + " AND SYSDATE BETWEEN gr.STARTPROMO AND (case when gr.STARTPROMO = gr.ENDPROMO then sysdate else NVL(gr.ENDPROMO,sysdate)  end) "
                                + " ORDER BY pc.id,tipo_promo,NVL(gr.PromotionPriority, 0) DESC, extrapropmeaning_id NULLS LAST, extraproplvalue NULLS LAST, promotion_id DESC";

                                respuesta = bddTytan.EjecutarConsulta("SELECT pc.id idgrupopromo, pc.descripcion desgrupopromo,ABS(gr.Promotion_Id) Promotion_Id,pc.aplicativo_id aplicativoconfig,pc.tipo_promo tipopromoconfig,"
                                + "('" + nomprod + "'||(case when gr.tiposerviciohd_id IS NOT NULL OR gr.tiposervicio_id IS NOT NULL then ' '||"
                                + "(select Name from trepvaluelistitems where valuelist_id = 500151 and id = nvl(" + citem.Controlnames[10].Valor + "," + citem.Controlnames[9].Valor + ")) end))Promotion_Code,"
                                + "gr.StartPromo, gr.EndPromo, gr.DFrom, gr.DTo, PromotionMonth, ExtraPropMeaning_id, ExtraProplValue, PromotionPriority,"
                                + " (case when (select count(id) from y_catalogpromotionsitems pi where pi.catalogpromotions_id = ABS(gr.Promotion_Id) and pi.controlname ='txtServicio') >0 then 2  else 1 end) tipo_promo,"
                                + "('['||"
                                + " substr((','||pc.tipo_promo||','),(instr((','||pc.tipo_promo||','),',',1,(REGEXP_COUNT((substr(','||pc.aplicativo_id||',',1,(instr(','||pc.aplicativo_id||',',',' ||" + aplicaion.Id + "|| ',')))),',')))+1), "
                                + " (instr((',' || pc.tipo_promo || ','), ',', 1, (REGEXP_COUNT((substr(',' || pc.aplicativo_id || ',', 1, (instr(',' || pc.aplicativo_id || ',', ',' ||" + aplicaion.Id + "|| ',')))), ',') + 1)) "
                                + "  - instr((',' || pc.tipo_promo || ','), ',', 1, (REGEXP_COUNT((substr(',' || pc.aplicativo_id || ',', 1, (instr(',' || pc.aplicativo_id || ',',',' ||" + aplicaion.Id + "|| ',')))), ','))) - 1 ) )"
                                + "||'[') tipo_promo_apli,"
                                + "(case when gr.extrapropmeaning_id > 0  then "
                                + "(case when gr.extrapropmeaning_id =" + citem.Controlnames[14].Id + " then "
                                + "(case when instr(gr.extraproplvalue, ',' ||" + citem.Controlnames[14].Valor + "|| ',') > 0 then 1  else 0 end)"
                                + "else 0 end)"
                                + "else 1 end)pareado "
                                + " FROM xPromotionPremises gr,YTBL_PROMOCOMERCIAL pc"
                                + " WHERE pc.id = " + dr["ID"] + ""
                                + " and instr((','||pc.promo_id||','),(','|| abs(gr.promotion_id)||',')) >0  and instr((',' || pc.aplicativo_id || ','), ('," + aplicaion.Id + ",')) > 0"
                                + " AND (gr.tipoaccount_id IS NULL OR gr.tipoaccount_id LIKE '%,' ||" + (citem.Controlnames[8].Valor).ToString() + "|| ',%')"
                                + " AND (gr.tariffplan_id IS NULL OR gr.tariffplan_id LIKE '%,' || " + (citem.Controlnames[5].Valor).ToString() + "|| ',%')"
                                + " AND (gr.product_id IS NULL OR gr.product_id LIKE '%,' ||" + (citem.Controlnames[12].Valor).ToString() + " || ',%')"
                                + " AND (gr.formapago_id IS NULL OR gr.formapago_id LIKE '%,' ||" + (citem.Controlnames[2].Valor).ToString() + " || ',%')"
                                + " AND (gr.negocio_id IS NULL OR gr.negocio_id LIKE '%,' ||" + (citem.Controlnames[3].Valor).ToString() + "|| ',%')"
                                + " AND (gr.nodo_id IS NULL OR gr.nodo_id LIKE '%,' ||" + (citem.Controlnames[4].Valor).ToString() + "|| ',%')"
                                + " AND (gr.ciudad_id IS NULL OR gr.ciudad_id LIKE '%,' ||" + (citem.Controlnames[1].Valor).ToString() + "|| ',%')"
                                + " AND(gr.tiposerviciohd_id IS NULL OR gr.tiposerviciohd_id LIKE '%,' ||" + (citem.Controlnames[10].Valor).ToString() + "|| ',%')"
                                + " AND(gr.tiposervicio_id IS NULL OR gr.tiposervicio_id LIKE '%,' ||" + (citem.Controlnames[9].Valor).ToString() + "|| ',%')"
                                + " AND (gr.clficacioncliente_id IS NULL OR gr.clficacioncliente_id LIKE '%,' ||" + (citem.Controlnames[0].Valor).ToString() + "|| ',%')"
                                + " AND (gr.tipocontratacion_id IS NULL OR gr.tipocontratacion_id LIKE '%,' ||" + (citem.Controlnames[7].Valor).ToString() + "|| ',%')"
                                + " AND(gr.tipodecodificador_id IS NULL OR gr.tipodecodificador_id LIKE '%,' ||" + (citem.Controlnames[11].Valor).ToString() + "|| ',%')"
                                + " AND SYSDATE BETWEEN gr.STARTPROMO AND (case when gr.STARTPROMO = gr.ENDPROMO then sysdate else NVL(gr.ENDPROMO,sysdate)  end) "
                                + " ORDER BY pc.id,tipo_promo,NVL(gr.PromotionPriority, 0) DESC, extrapropmeaning_id NULLS LAST, extraproplvalue NULLS LAST, promotion_id DESC", out datos2);

                                switch ((citem.Controlnames[9].Valor).ToString())
                                {
                                    case "505956":
                                        numDecoder = 1;
                                        break;
                                    case "505957":
                                        numDecoder = 2;
                                        break;
                                    case "505958":
                                        numDecoder = 3;
                                        break;
                                    case "505959":
                                        numDecoder = 4;
                                        break;
                                    case "505960":
                                        numDecoder = 5;
                                        break;
                                    case "505961":
                                        numDecoder = 6;
                                        break;
                                    case "505962":
                                        numDecoder = 7;
                                        break;
                                }

                                if (datos2.Rows.Count > 0)
                                {
                                    IEnumerable<DataRow> datosval = from fila in datos2.AsEnumerable()
                                                                    where fila.Field<decimal>("pareado") == 1   
                                                                     && (fila.Field<string>("tipo_promo_apli").Contains("[" + fila.Field<decimal>("tipo_promo") + "[")
                                                                     || fila.Field<string>("tipopromoconfig") is null)
                                                                    select fila;

                                    //var datosval = datos2.Select(" pareado=" + 1    );
                                    aplicaPromo = 1;

                                    if (datosval.Count() > 0)
                                    {   
                                        foreach (DataRow dr2 in datosval.ToList())
                                        {
                                            if (!(citem.Controlnames[12].Valor).ToString().Equals("5") || !(citem.Controlnames[12].Valor).ToString().Equals("170"))
                                            {
                                                tipoServ = "505956";
                                            }
                                            else
                                            {
                                                tipoServ = (citem.Controlnames[9].Valor).ToString();
                                            }
                                            string sentencia = "ln_precio := YPKG_APISTV.FN_RETORNA_VALSERVICO(" + "null,0," + dr2["tipo_promo"].ToString() + "," + (citem.Controlnames[1].Valor).ToString() + "," + (citem.Controlnames[8].Valor).ToString() +
                                                "," + (citem.Controlnames[5].Valor).ToString() +
                                                "," + (citem.Controlnames[12].Valor).ToString() + "," + (citem.Controlnames[3].Valor).ToString() + ","
                                                + numDecoder + "," + (citem.Controlnames[2].Valor).ToString() + ",sysdate," + tipoServ + "," + (citem.Controlnames[11].Valor).ToString() + "); " +
                                                " DBMS_OUTPUT.PUT_LINE(ln_precio);";
                                            string sentenciaArmada = bddTytan.ArmarSentencia(sentencia);
                                            respuesta = bddTytan.EjecutarSentenciaDBMS(sentenciaArmada, out valorProd);

                                            valorBase = Math.Round(Convert.ToDouble(valorProd), 2, MidpointRounding.AwayFromZero);

                                            if (Convert.ToInt32(dr2["tipo_promo"]) == 1 || Convert.ToInt32(dr2["tipo_promo"]) == 2)
                                            {
                                                bddTytan.EjecutarConsulta("select id,controlname, nvl(numbervalue, charvalue) as valor from Y_CATALOGPROMOTIONSITEMS T2 where 1 = 1 and catalogpromotions_id = " + Convert.ToInt32(dr2["Promotion_Id"]) + " and controlname in ('cblMesesPromocion', 'txtServicio','txtInstalacion') order by controlname asc,to_number(valor) ", out datosCtrlName);

                                                foreach (DataRow drctrlName in datosCtrlName.Rows)
                                                {
                                                    if (drctrlName["controlname"].ToString() == "cblMesesPromocion")
                                                    {
                                                        meses = meses + drctrlName["valor"].ToString() + ",";
                                                    }
                                                    if (drctrlName["controlname"].ToString() == "txtInstalacion")
                                                    {
                                                        porcentaje = drctrlName["valor"].ToString();
                                                    }
                                                    if (drctrlName["controlname"].ToString() == "txtServicio")
                                                    {
                                                        porcentaje = drctrlName["valor"].ToString();
                                                    }

                                                    if (meses == "" && Convert.ToInt32(dr2["tipo_promo"]) == 2) meses = "Por Siempre";


                                                }

                                                if (dr2["Promotion_Code"].ToString().Trim().Equals("HFC Deco"))
                                                {
                                                    bddTytan.EjecutarConsulta("select id,controlname, nvl(numbervalue, charvalue) as valor from Y_CATALOGPROMOTIONSITEMS T2 where 1 = 1 and catalogpromotions_id = " + Convert.ToInt32(dr2["Promotion_Id"]) + " and controlname in ('cblTipoServicio', 'cblTipoServicioHD') order by controlname asc,to_number(valor) ", out datosCtrlName);

                                                    foreach (DataRow drctrlNameTipServ in datosCtrlName.Rows)
                                                    {
                                                        if ((citem.Controlnames[10].Valor).ToString().Equals(drctrlNameTipServ["valor"].ToString()) ||
                                                            (citem.Controlnames[9].Valor).ToString().Equals(drctrlNameTipServ["valor"].ToString()))
                                                        {
                                                            switch (drctrlNameTipServ["valor"].ToString())
                                                            {
                                                                case "505956":
                                                                    tipoServAdic = "Principal";
                                                                    break;
                                                                case "505957":
                                                                    tipoServAdic = "1er Adicional";
                                                                    break;
                                                                case "505958":
                                                                    tipoServAdic = "2do Adicional";
                                                                    break;
                                                                case "505959":
                                                                    tipoServAdic = "3er Adicional";
                                                                    break;
                                                                case "505960":
                                                                    tipoServAdic = "4to Adicional";
                                                                    break;
                                                                case "505961":
                                                                    tipoServAdic = "5to Adicional";
                                                                    break;
                                                                case "505962":
                                                                    tipoServAdic = "6to Adicional";
                                                                    break;
                                                            }
                                                        }                                                        

                                                    }

                                                }

                                            }
                                            //valorProd = valorProd.Replace(",",".");
                                            //porcentaje = porcentaje.Replace(",", ".");
                                            totalTmp = Convert.ToDouble(valorBase) - ((Convert.ToDouble(valorBase) * Convert.ToDouble(porcentaje)) / 100);
                                                                                        
                                            totalTmp = Math.Round(totalTmp, 2, MidpointRounding.AwayFromZero);
                                            
                                               // totalTmp = (Math.Ceiling(totalTmp * 100) / 100);                                                                                 
                                            

                                            if (Convert.ToInt32(dr2["tipo_promo"]) == 2)
                                            {
                                                if (meses.Equals("0,"))
                                                {
                                                    descriPromo = "Gratis mensualidad en los días gozados del producto " + dr2["Promotion_Code"].ToString() + " " + tipoServAdic + ".";
                                                }else 
                                                    if (meses.Equals("Por Siempre"))
                                                    {
                                                        descriPromo = porcentaje + " % de descuento en mensualidad del producto " + dr2["Promotion_Code"].ToString() + " " + tipoServAdic + " Por Siempre.";
                                                    }
                                                    else
                                                    {
                                                    meses = meses.Substring(0, meses.LastIndexOf(","));
                                                     string[] mesesInfo = meses.Split(',');
                                                    foreach (string info in mesesInfo)
                                                    {
                                                        //Console.WriteLine("   {0}", info.Substring(info.IndexOf(": ") + 1));
                                                        meses = info;
                                                    }
                                                    descriPromo = porcentaje + " % de descuento por "+ meses +" mes(es) del producto " + dr2["Promotion_Code"].ToString() + " " + tipoServAdic + ".";
                                                }
                                                
                                            }

                                            if (Convert.ToInt32(dr2["tipo_promo"]) == 1)
                                            {
                                                if (porcentaje.Equals("100"))
                                                {
                                                    descriPromo = "Gratis instalacion en producto " + dr2["Promotion_Code"].ToString() + ".";
                                                }
                                            }


                                            //if (citemaux != (pcitem_id + dr2["tipo_promo"].ToString()))
                                            //{
                                            tipoPromo = Convert.ToInt32(dr2["tipo_promo"]); 
                                            citemaux = pcitem_id + dr2["tipo_promo"].ToString();
                                            promo = new Promocion();
                                            promo.Id = Convert.ToInt32(dr2["Promotion_Id"]);
                                            promo.tipo = Convert.ToInt32(dr2["tipo_promo"]);
                                            promo.citem_id = Convert.ToInt32(pcitem_id);
                                            promo.Descripcion = dr2["Promotion_Code"].ToString() + " " + tipoServAdic;
                                            promo.StartPromo = Convert.ToDateTime(dr2["StartPromo"]);
                                            promo.EndPromo = Convert.ToDateTime(dr2["EndPromo"]);
                                            promo.ValorBase = Convert.ToString(valorBase);
                                            promo.ValorProducto = Convert.ToString(totalTmp);
                                            promo.DescripPromo = descriPromo;
                                            promo.ObtenerDetallePromo();
                                            promo.controlnames = citem.Controlnames;
                                            promocom.Promociones.Add(promo);
                                            if (Convert.ToInt32(dr2["tipo_promo"]) == 2)
                                            {
                                                listMeses.Add(meses);
                                            }
                                            meses = "";
                                            porcentaje = "";
                                            tipoServAdic = string.Empty;

                                            
                                        }
                                        
                                        if (tipoPromo == 1)
                                        {
                                            switch ((citem.Controlnames[9].Valor).ToString())
                                            {
                                                case "505956":
                                                    tipoServAdic = "Principal";
                                                    break;
                                                case "505957":
                                                    tipoServAdic = "1er Adicional";
                                                    break;
                                                case "505958":
                                                    tipoServAdic = "2do Adicional";
                                                    break;
                                                case "505959":
                                                    tipoServAdic = "3er Adicional";
                                                    break;
                                                case "505960":
                                                    tipoServAdic = "4to Adicional";
                                                    break;
                                                case "505961":
                                                    tipoServAdic = "5to Adicional";
                                                    break;
                                                case "505962":
                                                    tipoServAdic = "6to Adicional";
                                                    break;
                                            }

                                            if (!(citem.Controlnames[12].Valor).ToString().Equals("5") && !(citem.Controlnames[12].Valor).ToString().Equals("170"))
                                            {
                                                tipoServ = "505956";
                                            }
                                            else
                                            {
                                                tipoServ = (citem.Controlnames[9].Valor).ToString();
                                            }
                                            string sentencia = "ln_precio := YPKG_APISTV.FN_RETORNA_VALSERVICO(" + "null,0,2," + (citem.Controlnames[1].Valor).ToString() + "," + (citem.Controlnames[8].Valor).ToString() +
                                            "," + (citem.Controlnames[5].Valor).ToString() +
                                            "," + (citem.Controlnames[12].Valor).ToString() + "," + (citem.Controlnames[3].Valor).ToString() + ","
                                            + numDecoder + "," + (citem.Controlnames[2].Valor).ToString() + ",sysdate," + tipoServ + "," + (citem.Controlnames[11].Valor).ToString() + "); " +
                                            " DBMS_OUTPUT.PUT_LINE(ln_precio);";
                                            string sentenciaArmada = bddTytan.ArmarSentencia(sentencia);
                                            respuesta = bddTytan.EjecutarSentenciaDBMS(sentenciaArmada, out valorProd);

                                            valorBase = Math.Round(Convert.ToDouble(valorProd), 2, MidpointRounding.AwayFromZero);

                                            totalTmp = Math.Round(Convert.ToDouble(valorBase), 2, MidpointRounding.AwayFromZero);

                                            bddTytan.EjecutarConsulta("select name from tpcproducts where id = " + citem.Controlnames[12].Valor.ToString(), out datosProd);

                                            prodName = datosProd.Rows[0]["name"].ToString();

                                            if ((citem.Controlnames[12].Valor).ToString().Equals("5") || (citem.Controlnames[12].Valor).ToString().Equals("170"))
                                            {
                                                descriPromo = "Valor regular del producto " + prodName + " " + tipoServAdic + ": $" + valorBase;
                                            }
                                            else
                                            {
                                                descriPromo = "Valor regular del producto " + prodName + ": $" + valorBase;
                                            }

                                            citemaux = Convert.ToString(pcitem_id);
                                            promo = new Promocion();
                                            promo.Id = 0;
                                            promo.tipo = 2;
                                            promo.citem_id = Convert.ToInt32(pcitem_id);
                                            promo.Descripcion = prodName + " " + tipoServAdic;
                                            promo.StartPromo = DateTime.Now;
                                            promo.EndPromo = DateTime.Now;
                                            promo.ValorBase = Convert.ToString(valorBase);
                                            promo.ValorProducto = Convert.ToString(totalTmp);
                                            promo.DescripPromo = descriPromo;
                                            promocom.Promociones.Add(promo);
                                            tipoServAdic = string.Empty;
                                        }

                                        meses = "";
                                        porcentaje = "";
                                        tipoServAdic = string.Empty;
                                        tipoPromo = 0;
                                    }
                                }
                                else
                                {
                                    if (aplicaPromo > 0) {
                                        switch ((citem.Controlnames[9].Valor).ToString())
                                        {
                                            case "505956":
                                                tipoServAdic = "Principal";
                                                break;
                                            case "505957":
                                                tipoServAdic = "1er Adicional";
                                                break;
                                            case "505958":
                                                tipoServAdic = "2do Adicional";
                                                break;
                                            case "505959":
                                                tipoServAdic = "3er Adicional";
                                                break;
                                            case "505960":
                                                tipoServAdic = "4to Adicional";
                                                break;
                                            case "505961":
                                                tipoServAdic = "5to Adicional";
                                                break;
                                            case "505962":
                                                tipoServAdic = "6to Adicional";
                                                break;
                                        }

                                        if (!(citem.Controlnames[12].Valor).ToString().Equals("5") && !(citem.Controlnames[12].Valor).ToString().Equals("170"))
                                        {
                                            tipoServ = "505956";
                                        }
                                        else
                                        {
                                            tipoServ = (citem.Controlnames[9].Valor).ToString();
                                        }
                                        if (tipoPromo == 0)
                                        {
                                            tipoPromo = 1;
                                            string sentencia;
                                            while (tipoPromo < 3)
                                            {
                                                if (tipoPromo == 1 && ((citem.Controlnames[12].Valor).ToString().Equals("5") || (citem.Controlnames[12].Valor).ToString().Equals("170")))
                                                {
                                                   sentencia = "ln_precio := YPKG_APISTV.FN_RETORNA_VALSERVICO(" + "null,0," + tipoPromo + "," + (citem.Controlnames[1].Valor).ToString() + "," + (citem.Controlnames[8].Valor).ToString() +
                                                   "," + (citem.Controlnames[5].Valor).ToString() +
                                                   "," + (citem.Controlnames[12].Valor).ToString() + "," + (citem.Controlnames[3].Valor).ToString() + ","
                                                   + numDecoder + "," + (citem.Controlnames[2].Valor).ToString() + ",sysdate," + tipoServ + "," + (citem.Controlnames[11].Valor).ToString() + "); " +
                                                   " DBMS_OUTPUT.PUT_LINE(ln_precio);";
                                                }
                                                else
                                                {
                                                    tipoPromo = 2;
                                                    sentencia = "ln_precio := YPKG_APISTV.FN_RETORNA_VALSERVICO(" + "null,0," + tipoPromo + "," + (citem.Controlnames[1].Valor).ToString() + "," + (citem.Controlnames[8].Valor).ToString() +
                                                   "," + (citem.Controlnames[5].Valor).ToString() +
                                                   "," + (citem.Controlnames[12].Valor).ToString() + "," + (citem.Controlnames[3].Valor).ToString() + ","
                                                   + numDecoder + "," + (citem.Controlnames[2].Valor).ToString() + ",sysdate," + tipoServ + "," + (citem.Controlnames[11].Valor).ToString() + "); " +
                                                   " DBMS_OUTPUT.PUT_LINE(ln_precio);";
                                                }
                                                   
                                                string sentenciaArmada = bddTytan.ArmarSentencia(sentencia);
                                                respuesta = bddTytan.EjecutarSentenciaDBMS(sentenciaArmada, out valorProd);

                                                valorBase = Math.Round(Convert.ToDouble(valorProd), 2, MidpointRounding.AwayFromZero);

                                                totalTmp = Math.Round(Convert.ToDouble(valorBase), 2, MidpointRounding.AwayFromZero);

                                                bddTytan.EjecutarConsulta("select name from tpcproducts where id = " + citem.Controlnames[12].Valor.ToString(), out datosProd);

                                                prodName = datosProd.Rows[0]["name"].ToString();

                                                if ((citem.Controlnames[12].Valor).ToString().Equals("5") || (citem.Controlnames[12].Valor).ToString().Equals("170"))
                                                {
                                                    if (tipoPromo == 1)
                                                    {
                                                        descriPromo = "Instalacion del producto " + prodName + " " + tipoServAdic + ": " + valorBase;
                                                    }
                                                    else
                                                    {
                                                        descriPromo = "Valor regular del producto " + prodName + " " + tipoServAdic + ": " + valorBase;
                                                    }                                                    
                                                }
                                                else
                                                {
                                                    descriPromo = "Valor regular del producto " + prodName + ": " + valorBase;
                                                }

                                                citemaux = Convert.ToString(pcitem_id);
                                                promo = new Promocion();
                                                promo.Id = 0;
                                                promo.tipo = tipoPromo;
                                                promo.citem_id = Convert.ToInt32(pcitem_id);
                                                promo.Descripcion = prodName + " " + tipoServAdic;
                                                promo.StartPromo = DateTime.Now;
                                                promo.EndPromo = DateTime.Now;
                                                promo.ValorBase = Convert.ToString(valorBase);
                                                promo.ValorProducto = Convert.ToString(totalTmp);
                                                promo.DescripPromo = descriPromo;
                                                promocom.Promociones.Add(promo);                                                
                                                tipoPromo++;
                                            }
                                            tipoServAdic = string.Empty;
                                            tipoPromo = 0;
                                        }                                        
                                    }
                                    
                                }

                                //verificacion para promo no contratadas
                                //if (citem.Citem_id.ToString() == "139823852" && dr["id"].ToString() =="1") Console.WriteLine("2");
                                citemaux = string.Empty;
                                string v_caption = string.Empty;
                                string v_valorpropiedad = string.Empty;
                                if (citem.Controlnames[14].Id.ToString() != "NULL" && citem.Controlnames[14].Valor.ToString() != "NULL")
                                {
                                    respuesta = bddTytan.EjecutarConsulta(" SELECT a.caption, (case when a.valuelist_id is null then '" + citem.Controlnames[14].Valor + "' "
                                    + "else (select name from trepvaluelistitems where id =" + citem.Controlnames[14].Valor + ") end) valorprop "
                                    + " FROM trepprops a where propertymeaning_id = " + citem.Controlnames[14].Id + " and rownum = 1 ", out datos);
                                }
                                if (datos.Rows.Count > 0)
                                {
                                    v_caption = datos.Rows[0]["caption"].ToString();
                                    v_valorpropiedad = datos.Rows[0]["valorprop"].ToString();
                                }

                                if (dr["ID"].ToString() == "18" ) citemaux = "0";

                                string query = "SELECT pc.id idgrupopromo, pc.descripcion desgrupopromo,pc.aplicativo_id,pa.Id Promotion_Id,"
                                + "('UPGRADE '|| '" + nomprod + "')Promotion_Code,"
                                + "PA.datefrom, pa.dateto, pa.DFrom, pa.DTo, pa.validnumbermonths, pa.aditional_properties, "
                                + "pc.tipo_promo tipopromoconfig,3 tipo_promo,"
                                + "('['||"
                                + " substr((','||pc.tipo_promo||','),(instr((','||pc.tipo_promo||','),',',1,(REGEXP_COUNT((substr(','||pc.aplicativo_id||',',1,(instr(','||pc.aplicativo_id||',',',' ||" + aplicaion.Id + "|| ',')))),',')))+1), "
                                + " (instr((',' || pc.tipo_promo || ','), ',', 1, (REGEXP_COUNT((substr(',' || pc.aplicativo_id || ',', 1, (instr(',' || pc.aplicativo_id || ',', ',' ||" + aplicaion.Id + "|| ',')))), ',') + 1)) "
                                + "  - instr((',' || pc.tipo_promo || ','), ',', 1, (REGEXP_COUNT((substr(',' || pc.aplicativo_id || ',', 1, (instr(',' || pc.aplicativo_id || ',',',' ||" + aplicaion.Id + "|| ',')))), ','))) - 1 ) )"
                                + "||'[') tipo_promo_apli,"
                                + "(case when pa.aditional_properties is not null    then "
                                + "(case when substr(pa.aditional_properties,1,(instr(pa.aditional_properties,'|')-1)) = '" + v_caption + "' then "
                                + "(case when instr((','||substr(pa.aditional_properties,(instr(pa.aditional_properties,'|')+1),length(pa.aditional_properties))||','), ('," + v_valorpropiedad + ",')) > 0 then  "
                                + "1 else 0 end) "
                                + "else 0 end) "
                                + "else 1 end)pareado "
                                + "from ytpromocionesadmin pa,YTBL_PROMOCOMERCIAL pc "
                                + " where pc.id = " + dr["ID"] + ""
                                + " and instr((','||pc.promoupgradeid||','),(','|| pa.id ||',')) >0 and instr((',' || pc.aplicativo_id || ','), ('," + aplicaion.Id + ",')) > 0"
                                + " AND SYSDATE BETWEEN pa.datefrom AND NVL(pa.dateto, SYSDATE)"
                                + " and (pa.customertype_id is null or pa.customertype_id like (select '%,' || cpartytype_id || ',%' from tamcpartyaccounttypes a where id = " + citem.Controlnames[8].Valor + ")) "
                                + " and (pa.score IS NULL OR pa.score like (select '%,'||name||',%' from trepvaluelistitems lv where lv.valuelist_id = 500084 and lv.id =" + citem.Controlnames[0].Valor + ")) "
                                + " and(pa.accounttype_id IS NULL OR pa.accounttype_id LIKE '%,' || " + (citem.Controlnames[8].Valor).ToString() + "|| ',%')"
                                + " and(pa.paymentmode_id IS NULL OR pa.paymentmode_id LIKE '%,' || TO_NUMBER(nvl(" + (citem.Controlnames[2].Valor).ToString() + ",0)) || ',%')"
                                + " and(pa.product_id IS NULL OR pa.product_id LIKE '%,' ||" + (citem.Controlnames[12].Valor).ToString() + "|| ',%')"
                                + " and(pa.tariffplan_id IS NULL OR pa.tariffplan_id LIKE '%,' ||" + (citem.Controlnames[6].Valor).ToString() + "|| ',%')"
                                + " and(pa.tariffplanvariant_id IS NULL OR pa.tariffplanvariant_id LIKE '%,' || TO_NUMBER(nvl(" + (citem.Controlnames[5].Valor).ToString() + ",0)) || ',%')"
                                + " and(pa.premisa_adiconal IS NULL OR pa.premisa_adiconal LIKE '%,' ||" + (citem.Controlnames[16].Valor).ToString() + "|| ',%')"
                                + " and(pa.city_id IS NULL OR pa.city_id LIKE '%,' ||" + (citem.Controlnames[1].Valor).ToString() + "|| ',%')"
                                + " and(pa.accountcategory IS NULL OR ','||pa.accountcategory||',' LIKE '%,'|| " + (citem.Controlnames[15].Valor).ToString() + "|| ',%')"
                                + " and(pa.combinationtype IS NULL OR pa.combinationtype LIKE '%'||" + (citem.Controlnames[3].Valor).ToString() + "|| ',%')"
                                + "and( pa.accesstechnology_type IS NULL OR pa.accesstechnology_type  LIKE '%'||" + (citem.Controlnames[13].Valor).ToString() + ")"
                                  + " order by pa.dfrom desc";

                                respuesta = bddTytan.EjecutarConsulta("SELECT pc.id idgrupopromo, pc.descripcion desgrupopromo,pc.aplicativo_id,pa.Id Promotion_Id,"
                                + "('UPGRADE '|| '" + nomprod + "')Promotion_Code,"
                                + "PA.datefrom, pa.dateto, pa.DFrom, pa.DTo, pa.validnumbermonths, pa.aditional_properties, "
                                + "pc.tipo_promo tipopromoconfig,3 tipo_promo,"
                                + "('['||"
                                + " substr((','||pc.tipo_promo||','),(instr((','||pc.tipo_promo||','),',',1,(REGEXP_COUNT((substr(','||pc.aplicativo_id||',',1,(instr(','||pc.aplicativo_id||',',',' ||" + aplicaion.Id + "|| ',')))),',')))+1), "
                                + " (instr((',' || pc.tipo_promo || ','), ',', 1, (REGEXP_COUNT((substr(',' || pc.aplicativo_id || ',', 1, (instr(',' || pc.aplicativo_id || ',', ',' ||" + aplicaion.Id + "|| ',')))), ',') + 1)) "
                                + "  - instr((',' || pc.tipo_promo || ','), ',', 1, (REGEXP_COUNT((substr(',' || pc.aplicativo_id || ',', 1, (instr(',' || pc.aplicativo_id || ',',',' ||" + aplicaion.Id + "|| ',')))), ','))) - 1 ) )"
                                + "||'[') tipo_promo_apli,"
                                + "(case when pa.aditional_properties is not null    then "
                                + "(case when substr(pa.aditional_properties,1,(instr(pa.aditional_properties,'|')-1)) = '" + v_caption + "' then "
                                + "(case when instr((','||substr(pa.aditional_properties,(instr(pa.aditional_properties,'|')+1),length(pa.aditional_properties))||','), ('," + v_valorpropiedad + ",')) > 0 then  "
                                + "1 else 0 end) "
                                + "else 0 end) "
                                + "else 1 end)pareado "
                                + "from ytpromocionesadmin pa,YTBL_PROMOCOMERCIAL pc "
                                + " where pc.id = " + dr["ID"] + ""
                                + " and instr((','||pc.promoupgradeid||','),(','|| pa.id ||',')) >0 and instr((',' || pc.aplicativo_id || ','), ('," + aplicaion.Id + ",')) > 0"
                                + " AND SYSDATE BETWEEN pa.datefrom AND NVL(pa.dateto, SYSDATE)"
                                + " and (pa.customertype_id is null or pa.customertype_id like (select '%,' || cpartytype_id || ',%' from tamcpartyaccounttypes a where id = " + citem.Controlnames[8].Valor + ")) "                                                                   
                                + " and (pa.score IS NULL OR pa.score like (select '%,'||name||',%' from trepvaluelistitems lv where lv.valuelist_id = 500084 and lv.id =" + citem.Controlnames[0].Valor + ")) "                                
                                + " and(pa.accounttype_id IS NULL OR pa.accounttype_id LIKE '%,' || " + (citem.Controlnames[8].Valor).ToString() + "|| ',%')"
                                + " and(pa.paymentmode_id IS NULL OR pa.paymentmode_id LIKE '%,' || TO_NUMBER(nvl(" + (citem.Controlnames[2].Valor).ToString() + ",0)) || ',%')"
                                + " and(pa.product_id IS NULL OR pa.product_id LIKE '%,' ||" + (citem.Controlnames[12].Valor).ToString() + "|| ',%')"
                                + " and(pa.tariffplan_id IS NULL OR pa.tariffplan_id LIKE '%,' ||" + (citem.Controlnames[6].Valor).ToString() + "|| ',%')"
                                + " and(pa.tariffplanvariant_id IS NULL OR pa.tariffplanvariant_id LIKE '%,' || TO_NUMBER(nvl(" + (citem.Controlnames[5].Valor).ToString() + ",0)) || ',%')"
                                + " and(pa.premisa_adiconal IS NULL OR pa.premisa_adiconal LIKE '%,' ||" + (citem.Controlnames[16].Valor).ToString() + "|| ',%')"
                                + " and(pa.city_id IS NULL OR pa.city_id LIKE '%,' ||" + (citem.Controlnames[1].Valor).ToString() + "|| ',%')"
                                + " and(pa.accountcategory IS NULL OR ','||pa.accountcategory||',' LIKE '%,'|| " + (citem.Controlnames[15].Valor).ToString() + "|| ',%')"
                                + " and(pa.combinationtype IS NULL OR pa.combinationtype LIKE '%'||" + (citem.Controlnames[3].Valor).ToString() + "|| ',%')"                                
                                + "and( pa.accesstechnology_type IS NULL OR pa.accesstechnology_type  LIKE '%'||"+ (citem.Controlnames[13].Valor).ToString()+")"                                
                                  + " order by pa.dfrom desc", out datos3);
                                                                                                                

                                if (datos3.Rows.Count > 0)
                                {
                                    //var datosval = datos3.Select("pareado=" + 1);

                                    IEnumerable<DataRow> datosval = from fila in datos3.AsEnumerable()
                                                                    where fila.Field<decimal>("pareado") == 1
                                                                     && (fila.Field<string>("tipo_promo_apli").Contains("[" + fila.Field<decimal>("tipo_promo") + "[")
                                                                      || fila.Field<string>("tipopromoconfig") is null)
                                                                    //|| fila.Field<string>("tipopromoconfig") == ",")
                                                                    //&& fila.Field<string>("tipo_promo_apli").IndexOf("|2|"          ) > 0
                                                                    select fila;

                                    if (datosval.Count() > 0)
                                    {
                                        citemaux = "0";
                                        foreach (DataRow dr2 in datosval.ToList())
                                        {
                                            if (citemaux != (pcitem_id.ToString() + dr2["tipo_promo"].ToString()))
                                            {
                                               // citemaux = pcitem_id.ToString() + dr2["tipo_promo"].ToString();
                                                promo = new Promocion();
                                                promo.Id = Convert.ToInt32(dr2["Promotion_Id"]);
                                                promo.Tipo = Convert.ToInt32(dr2["tipo_promo"]);    
                                                promo.Citem_id = Convert.ToInt32(pcitem_id);
                                                promo.Descripcion = dr2["Promotion_Code"].ToString();
                                                promo.StartPromo = Convert.ToDateTime(dr2["DateFrom"]);
                                                promo.EndPromo = Convert.ToDateTime(dr2["DateTo"]);
                                                promo.ObtenerDetallePromo();
                                                promo.controlnames = citem.Controlnames;
                                                promocom.Promociones.Add(promo);
                                            }
                                            
                                        }
                                    }

                                }

                                //}
                            }
                            int numMeses = listMeses.Count();
                            aplicaPromo = 0;
                            promocom.Id = Convert.ToInt32(dr["id"]);
                            promocom.Nombre = dr["descripcion"].ToString();
                            promocom.DescripcionPromo = string.Empty;
                            if (promocom.Promociones.Count > 0) promosComerciales.Add(promocom);                            
                        }
                    }
                }

                return "Ok";
            }
            catch (Exception ex)
            {
                //if (ex.Message == "") ex.Message = 'vacio';
                return ex.Message;
            }
            #endregion

        }
        //public string RegitraPromociones(PromoComercial promocom, out string respuesta)
        #endregion
    }
}
