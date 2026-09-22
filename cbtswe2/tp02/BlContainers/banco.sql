-- Ajustes recomendados no seu script para o CRUD funcionar (ID gerado automaticamente)
CREATE TABLE BL (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Numero VARCHAR(255) NOT NULL,
    Consignee VARCHAR(255),
    Navio VARCHAR(255)
);

CREATE TABLE CONTAINER (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    Numero CHAR(11) NOT NULL,
    Tipo VARCHAR(10) NOT NULL CHECK (Tipo IN ('Dry', 'Reefer')),
    Tamanho INT NOT NULL CHECK (Tamanho IN (20, 40)),
    BL_ID INT NOT NULL,  -- obrigatório: todo container pertence a um BL
    FOREIGN KEY (BL_ID) REFERENCES BL(ID)
);

-- Se as tabelas JÁ existem, use:
-- ALTER TABLE BL MODIFY ID INT AUTO_INCREMENT;
-- ALTER TABLE CONTAINER MODIFY ID INT AUTO_INCREMENT;
-- ALTER TABLE CONTAINER MODIFY BL_ID INT NOT NULL;
