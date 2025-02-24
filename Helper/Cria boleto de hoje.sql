INSERT INTO public.boletos(
    "Id",
    "NomePagador",
    "CPFCNPJPagador",
    "NomeBeneficiario",
    "CPFCNPJBeneficiario",
    "Valor",
    "DataVencimento",
    "Observacao",
    "BancoId"
)
VALUES (
    333,                              
    'Zeca',                
    '123.123.123-00',                   
    'NU',           
    '11.222.111/0000-00',                   
    200.00,                         
    CURRENT_DATE,  
    'Nenhuma',           
    1                               
);