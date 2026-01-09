import { RefObject, SyntheticEvent, useEffect, useMemo, useState } from 'react';
import Box from '@mui/material/Box';
import {
  DataGrid,
  GRID_CHECKBOX_SELECTION_COL_DEF,
  GridColDef
} from '@mui/x-data-grid';
import { GridApiCommunity } from '@mui/x-data-grid/internals';
import DashboardMenu from 'components/common/DashboardMenu';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import { IconifyIcon } from 'components/base/IconifyIcon';
import AccountDialog from '../account/common/AccountDialog';
import { SnackbarCloseReason, TextField } from '@mui/material';
import api from "../../../service/api";
import ProSnackbar from 'layouts/main-layout/common/ProSnackbar';

interface SubjectsTableProps {
  apiRef: RefObject<GridApiCommunity | null>;
  filterButtonEl?: HTMLButtonElement | null;
};

type Subject = {
  id: number;
  descricao: string;
};

const SubjectsTable = ({ apiRef }: SubjectsTableProps) => {
  const [subjects, setSubjects] = useState<Subject[]>([]);
  const [id, setId] = useState(0);
  const [description, setDescription] = useState("");
  const [submitted, setSubmitted] = useState(false);
  const [index, setIndex] = useState(-1);
  const [open, setOpen] = useState(false);
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [error, setError] = useState(false);
  const [openUpdate, setOpenUpdate] = useState(false);
  const [openDelete, setOpenDelete] = useState(false);

  useEffect(() => {
    (async () => {
      const response = await api.get("subject");
      setSubjects(response.data.data);
    })();
  }, []);
  
  const handleOpenUpdate = () => setOpenUpdate(!openUpdate);
  const handleOpenDelete = () => setOpenDelete(!openDelete);

  function handleOpen() {
    setSubmitted(false);
    setDescription("");
    setOpen(!open);
  }
  
  async function handleModalUpdate(index: number, id: number) {
    try{
      setIndex(index);
      setId(id);
      setOpenUpdate(true);
      const response = await api.get(`subject/${id}`);
      if(response.status === 200) {
        const { descricao } = response.data.data;
        setDescription(descricao);
      }
    }
    catch(error) {
      console.log(error)
    }
  }

  function handleModalDelete(index: number, id: number) {
    setIndex(index);
    setId(id);
    setOpenDelete(true);
  }

  async function saveSubject() {
    try {
      setSubmitted(true);

      if (!description) {
        return;
      }

      const response = await api.post("subject", {
        descricao: description
      });
      const newSubject = response.data.data;
      setSubjects(prev => [...prev, newSubject]);
      setError(false);
      setOpen(false);
    }
    catch(error) {
      setError(true);
      console.log(error);
    }
    finally {
      if(description) setOpenSnackbar(true);
    }
  }

  async function updateSubject() {
    try {
      setSubmitted(true);

      if (!description) {
        return;
      }
      
      const response = await api.put("subject", {
        id,
        descricao: description,
      });
      const updateSubject = response.data.data;
      setSubjects(prev => [
        ...prev.slice(0, index),
        updateSubject,
        ...prev.slice(index + 1),
      ]);
      setOpenUpdate(false);
    }
    catch(error) {
      console.log(error);
    }
  }

  async function deleteSubject() {
    try {  
      const response = await api.delete(`subject/${id}`);
      if(response.status === 200){
        setSubjects(prev => [
          ...prev.slice(0, index),
          ...prev.slice(index + 1),
        ]);
      }
    }
    catch(error) {
      console.log(error);
    }
    finally {
      setOpenDelete(false);
    }
  }

  const handleClose = (_event: SyntheticEvent, reason?: SnackbarCloseReason) => {
    if (reason === 'clickaway') return;
    setOpenSnackbar(false);
  };

  const columns: GridColDef<Subject>[] = useMemo(
    () => [
      {
        ...GRID_CHECKBOX_SELECTION_COL_DEF,
        width: 64,
      },
      {
        field: 'descricao',
        headerName: 'Descrição',
        minWidth: 160,
        sortable: false,
        filterable: false,
        flex: 1,
      },
      {
        field: 'action',
        headerName: '',
        filterable: false,
        sortable: false,
        width: 60,
        align: 'right',
        headerAlign: 'right',
        renderCell: (params) => {
          const index = params.api.getRowIndexRelativeToVisibleRows(params.id);
          return (
            <DashboardMenu
              menuItems={[
                {
                  label: 'Editar',
                  onClick: () => handleModalUpdate(index, params.row.id)
                },
                {
                  label: 'Remover',
                  sx: { color: 'error.main' },
                  onClick: () => handleModalDelete(index, params.row.id)
                },
              ]}
            />
          )
        },
      },
    ],
    [],
  );

  return (
    <Box sx={{ width: 1 }}>
      <Stack
        sx={{
          columnGap: 1,
          rowGap: 2,
          justifyContent: 'space-between',
          alignItems: { xl: 'center' },
          flexWrap: { xs: 'wrap', md: 'nowrap' },
          mb: 4
        }}
      >
        <Button
          variant="contained"
          color="primary"
          startIcon={<IconifyIcon icon="material-symbols:add-rounded" />}
          sx={{ flexShrink: 0 }}
          onClick={handleOpen}
          //onClick={() => setOpenSnackbar(true)}
        >
          Assunto
        </Button>
      </Stack>
      <DataGrid
        rowHeight={64}
        rows={subjects}
        apiRef={apiRef}
        columns={columns}
        hideFooter
      />

      <AccountDialog
        title="Novo assunto"
        open={open}
        handleConfirm={saveSubject}
        handleDialogClose={handleOpen}
        handleDiscard={handleOpen}
        sx={{ minWidth: 400, maxWidth: 600 }}
      >
        <Stack direction="column" spacing={1} p={0.125}>
          <TextField
            placeholder="Descrição"
            label="Descrição"
            required
            error={submitted && !description}
            helperText={submitted && !description ? "Campo obrigatório" : ""}
            onChange={e => setDescription(e.target.value)}
            slotProps={{
              htmlInput: {
                maxLength: 20
              },
            }}
            fullWidth
          />
        </Stack>
      </AccountDialog>
      
      <AccountDialog
        title="Editar assunto"
        open={openUpdate}
        handleConfirm={updateSubject}
        handleDialogClose={handleOpenUpdate}
        handleDiscard={handleOpenUpdate}
        sx={{ minWidth: 400, maxWidth: 600 }}
      >
        <Stack direction="column" spacing={1} p={0.125}>
          <TextField
            placeholder="Descrição"
            label="Descrição"
            required
            error={submitted && !description}
            helperText={submitted && !description ? "Campo obrigatório" : ""}
            value={description}
            onChange={e => setDescription(e.target.value)}
            slotProps={{
              htmlInput: {
                maxLength: 20,
              },
            }}
            fullWidth
          />
        </Stack>
      </AccountDialog>
      
      <AccountDialog
        title="Remover assunto"
        subtitle="Deseja confirmar a remoção do assunto?"
        open={openDelete}
        handleConfirm={deleteSubject}
        handleDialogClose={handleOpenDelete}
        handleDiscard={handleOpenDelete}
        typeDelete
        sx={{ minWidth: 400, maxWidth: 600 }}
      />
      <ProSnackbar open={openSnackbar} onClose={handleClose} error={error} />
    </Box>
  );
};

export default SubjectsTable;
